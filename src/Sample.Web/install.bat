sc create Sample binPath= "%~dp0Sample.Web.exe"
sc failure Sample actions= restart/60000/restart/60000/""/60000 reset= 86400
sc start Sample
sc config Sample start=auto