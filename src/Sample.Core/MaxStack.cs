using System;
using System.Collections.Generic;

namespace Sample.Core
{
    /// <summary>
    /// Generic stack implementation with a maximum limit
    /// When something is pushed on the last item is removed from the list
    /// </summary>
    [Serializable]
    public class MaxStack<T>
    {
        #region Fields

        private int _limit;
        private List<T> _list = null;

        #endregion Fields

        #region Constructors

        public MaxStack(int maxSize)
        {
            _limit = maxSize;
            _list = new List<T>();
        }

        #endregion Constructors

        //ilk eklenen en başta oluşturulur,kapasite dolunca ilk eklenen silinir, en son eklenen en sonda olacaktır
        public void Ekle(T value)
        {
            if (_list.Count == _limit)
            {
                _list.RemoveAt(0);
            }
            _list.Add(value);
        }

        //ilk sırada en eski ölçülen ölçüm olmak üzere ölçümler döndürülür
        public List<T> ReturnValues()
        {
            return _list;
        }
    }
}