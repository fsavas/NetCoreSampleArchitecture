using AutoMapper;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Caching.Memory;
using Sample.Core;
using Sample.Core.Defaults;
using Sample.Core.Helpers;
using Sample.Services.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using static Sample.Core.Defaults.EnumClasses;

namespace Sample.Services.ExportImport
{
    public partial class ExportManager<TModel, TEntity> : IExportManager<TModel, TEntity> where TModel : BaseGrid where TEntity : BaseEntity
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IEnumManager _enumManager;

        #endregion Fields

        #region Constructor

        public ExportManager(IMapper mapper, IMemoryCache memoryCache, IEnumManager enumManager)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _enumManager = enumManager;
        }

        #endregion Constructor

        #region Methods

        public string ExportToExcel(List<TEntity> list)
        {
            var data = _mapper.Map<List<TEntity>, List<TModel>>(list);

            if (data != null)
            {
                var modelType = typeof(TModel);
                var properties = modelType.GetProperties();

                if (properties != null)
                {
                    return GenerateExcel(data, properties);
                }
            }

            return null;
        }

        private string GenerateExcel(List<TModel> data, PropertyInfo[] properties)
        {
            var filePath = FileHelper.GetFilePath(typeof(TModel).Name, FileExtensionTypes.xlsx);

            using (SpreadsheetDocument spreadsheet = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                //create workbook part
                var wbp = spreadsheet.AddWorkbookPart();
                wbp.Workbook = new Workbook();

                //add style
                //AddStyleSheet(spreadsheet);

                var sheets = wbp.Workbook.AppendChild<Sheets>(new Sheets());

                //create worksheet part, and add it to the sheets collection in workbook
                var wsp = wbp.AddNewPart<WorksheetPart>();
                var sheet = new Sheet { Id = spreadsheet.WorkbookPart.GetIdOfPart(wsp), SheetId = 1, Name = MemoryCacheKeys.Sheet + "1" };//todo get from localization
                sheets.Append(sheet);

                var writer = OpenXmlWriter.Create(wsp);
                writer.WriteStartElement(new Worksheet());

                //Create columns
                writer.WriteStartElement(new Columns());
                foreach (var property in properties)
                {
                    writer.WriteElement(new Column { Min = Convert.ToUInt32(1), Max = Convert.ToUInt32(23), CustomWidth = true, Width = DoubleValue.FromDouble(20) });
                }
                writer.WriteEndElement();

                writer.WriteStartElement(new SheetData());

                writer.WriteStartElement(new Row());
                foreach (var property in properties)
                {
                    var attribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                        .Cast<DisplayNameAttribute>().FirstOrDefault();

                    if (attribute != null)
                    {
                        if (!string.IsNullOrEmpty(attribute.DisplayName) && !_memoryCache.TryGetValue(attribute.DisplayName, out string displayName))
                            writer.WriteElement(new Cell { CellValue = new CellValue(displayName), DataType = CellValues.String });
                        else
                            writer.WriteElement(new Cell { CellValue = new CellValue(attribute.DisplayName ?? ""), DataType = CellValues.String });
                    }
                    else
                    {
                        writer.WriteElement(new Cell { CellValue = new CellValue(""), DataType = CellValues.String });
                    }
                }
                writer.WriteEndElement();

                foreach (var item in data)
                {
                    writer.WriteStartElement(new Row());

                    for (int i = 0; i < properties.Length; i++)
                    {
                        string cellValue = "";
                        var property = item.GetType().GetProperties()[i];
                        var itemValue = property.GetValue(item);

                        if (itemValue != null)
                        {
                            if (itemValue.GetType().BaseType == typeof(Enum))
                            {
                                cellValue = _enumManager.GetDescription(itemValue);
                            }
                            else
                            {
                                cellValue = itemValue != null ? itemValue.ToString() : "";
                            }
                        }

                        writer.WriteElement(new Cell { CellValue = new CellValue(cellValue), DataType = CellValues.String });
                    }
                    writer.WriteEndElement();//end of row
                }

                writer.WriteEndElement();//end of sheetData

                //writer.WriteStartElement(new MergeCells());
                //writer.WriteElement(new MergeCell() { Reference = new StringValue("A1:E1") });
                //writer.WriteEndElement();

                writer.WriteEndElement();//end of worksheet
                writer.Close();
                spreadsheet.Close();
            }

            return filePath;
        }

        #endregion Methods
    }
}