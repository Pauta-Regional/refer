/* MFCREATE.C -- Metafile Creation Program */

#include <windows.h>

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE hInstance, hPrevInstance ;
     LPSTR  lpszCmdLine ;
     int    nCmdShow ;
     {
     HBRUSH hBrush  = CreateSolidBrush (RGB (0, 0, 255)) ;
     HDC    hMetaDC = CreateMetaFile ("MYLOGO.WMF") ;

     Rectangle (hMetaDC, 0, 0, 100, 100) ;
     MoveTo (hMetaDC, 0, 0) ;
     LineTo (hMetaDC, 100, 100) ;
     MoveTo (hMetaDC, 0, 100) ;
     LineTo (hMetaDC, 100, 0) ;
     SelectObject (hMetaDC, hBrush) ;
     Ellipse (hMetaDC, 20, 20, 80, 80) ;

     DeleteMetaFile (CloseMetaFile (hMetaDC)) ;
     DeleteObject (hBrush) ;

     MessageBeep (0) ;

     return FALSE ;
     }
