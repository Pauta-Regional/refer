/* FORMFEED.C -- Advances printer to next page */

#include <windows.h>
#include <string.h>

HDC  GetPrinterDC (void) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szMsg [] = "FormFeed" ; 
     HDC         hPrintDC ;

     if (hPrintDC = GetPrinterDC ())
          {
          if (Escape (hPrintDC, STARTDOC, sizeof szMsg - 1, szMsg, NULL) > 0)
               if (Escape (hPrintDC, NEWFRAME, 0, NULL, NULL) > 0)
                    Escape (hPrintDC, ENDDOC, 0, NULL, NULL) ;

          DeleteDC (hPrintDC) ;
          }
     return FALSE ;
     }

HDC GetPrinterDC ()
     {
     char szPrinter [64] ;
     char *szDevice, *szDriver, *szOutput ;

     GetProfileString ("windows", "device", "", szPrinter, 64) ;

     if ((szDevice = strtok (szPrinter, "," )) &&
         (szDriver = strtok (NULL,      ", ")) && 
         (szOutput = strtok (NULL,      ", ")))
          
               return CreateDC (szDriver, szDevice, szOutput, NULL) ;

     return 0 ;
     }
