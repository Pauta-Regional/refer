/* GRAFMENU.C -- Demonstrates Bitmap Menu Items */

#include <windows.h>
#include <string.h>
#include "grafmenu.h"

long FAR PASCAL WndProc  (HWND, unsigned, WORD, LONG) ;
HBITMAP StretchBitmap (HBITMAP) ;
HBITMAP GetBitmapFont (int) ;

char szAppName [] = "GrafMenu" ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE   hInstance, hPrevInstance ;
     LPSTR    lpszCmdLine ;
     int      nCmdShow ;
     {
     HBITMAP  hBitmapHelp, hBitmapFile, hBitmapEdit,
              hBitmapFont, hBitmapPopFont [3] ;
     HMENU    hMenu, hMenuPopup ;
     HWND     hWnd ;
     int      i ;
     MSG      msg ;
     WNDCLASS wndclass ;

     if (!hPrevInstance) 
          {
          wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
          wndclass.lpfnWndProc   = WndProc ;
          wndclass.cbClsExtra    = 0 ;
          wndclass.cbWndExtra    = 0 ;
          wndclass.hInstance     = hInstance ;
          wndclass.hIcon         = LoadIcon (NULL, IDI_APPLICATION) ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
          wndclass.hbrBackground = GetStockObject (WHITE_BRUSH) ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }

     hMenu = CreateMenu () ;

     hMenuPopup = LoadMenu (hInstance, "MenuFile") ;
     hBitmapFile = StretchBitmap (LoadBitmap (hInstance, "BitmapFile")) ;
     ChangeMenu (hMenu, NULL, (PSTR) hBitmapFile, hMenuPopup,
                                        MF_APPEND | MF_BITMAP | MF_POPUP) ;

     hMenuPopup = LoadMenu (hInstance, "MenuEdit") ;
     hBitmapEdit = StretchBitmap (LoadBitmap (hInstance, "BitmapEdit")) ;
     ChangeMenu (hMenu, NULL, (PSTR) hBitmapEdit, hMenuPopup,
                                        MF_APPEND | MF_BITMAP | MF_POPUP) ;

     hMenuPopup = CreateMenu () ;
     for (i = 0 ; i < 3 ; i++)
          {
          hBitmapPopFont [i] = GetBitmapFont (i) ;
          ChangeMenu (hMenuPopup, NULL, (PSTR) hBitmapPopFont [i],
                                        IDM_COUR + i, MF_APPEND | MF_BITMAP) ;
          }

     hBitmapFont = StretchBitmap (LoadBitmap (hInstance, "BitmapFont")) ;
     ChangeMenu (hMenu, NULL, (PSTR) hBitmapFont, hMenuPopup,
                                        MF_APPEND | MF_BITMAP | MF_POPUP) ;

     hWnd = CreateWindow (szAppName, "Bitmap Menu Demonstration",
                         WS_OVERLAPPEDWINDOW,
                         CW_USEDEFAULT, 0,
                         CW_USEDEFAULT, 0,
                         NULL, hMenu, hInstance, NULL) ;

     hMenu = GetSystemMenu (hWnd, FALSE);
     hBitmapHelp = StretchBitmap (LoadBitmap (hInstance, "BitmapHelp")) ;
     ChangeMenu (hMenu, NULL, NULL, 0, MF_APPEND | MF_SEPARATOR);
     ChangeMenu (hMenu, NULL, (PSTR) hBitmapHelp, IDM_HELP, 
                                        MF_APPEND | MF_BITMAP) ;

     ShowWindow (hWnd, nCmdShow) ;
     UpdateWindow (hWnd) ;

     while (GetMessage (&msg, NULL, 0, 0))
          {
          TranslateMessage (&msg) ;
          DispatchMessage (&msg) ;
          }
     DeleteObject (hBitmapHelp) ;
     DeleteObject (hBitmapEdit) ;
     DeleteObject (hBitmapFile) ;
     DeleteObject (hBitmapFont) ;

     for (i = 0 ; i < 3 ; i++)
          DeleteObject (hBitmapPopFont [i]) ;

     return msg.wParam ;
     }

HBITMAP StretchBitmap (hBitmap1)
     HBITMAP    hBitmap1 ;
     {
     BITMAP     bm1, bm2 ;
     HBITMAP    hBitmap2 ;
     HDC        hDC, hMemDC1, hMemDC2 ;
     TEXTMETRIC tm ;

     hDC = CreateIC ("DISPLAY", NULL, NULL, NULL) ;
     GetTextMetrics (hDC, &tm) ;
     hMemDC1 = CreateCompatibleDC (hDC) ;
     hMemDC2 = CreateCompatibleDC (hDC) ;
     DeleteDC (hDC) ;

     GetObject (hBitmap1, sizeof (BITMAP), (LPSTR) &bm1) ;

     bm2 = bm1 ;
     bm2.bmWidth      = (tm.tmAveCharWidth * bm2.bmWidth)  / 4 ;
     bm2.bmHeight     = (tm.tmHeight       * bm2.bmHeight) / 8 ;
     bm2.bmWidthBytes = ((bm2.bmWidth + 15) / 16) * 2 ;

     hBitmap2 = CreateBitmapIndirect (&bm2) ;

     SelectObject (hMemDC1, hBitmap1) ;
     SelectObject (hMemDC2, hBitmap2) ;

     StretchBlt (hMemDC2, 0, 0, bm2.bmWidth, bm2.bmHeight,
                 hMemDC1, 0, 0, bm1.bmWidth, bm1.bmHeight, SRCCOPY) ;

     DeleteDC (hMemDC1) ;
     DeleteDC (hMemDC2) ;
     DeleteObject (hBitmap1) ;

     return hBitmap2 ;
     }

HBITMAP GetBitmapFont (i)
     int     i ;
     {
     static  struct
          {
          BYTE lfPitchAndFamily ;
          BYTE lfFaceName [LF_FACESIZE] ;
          char *szMenuText ;
          } 
          lfSet [3] =
          {
          FIXED_PITCH    | FF_MODERN, "Courier",   "Courier",
          VARIABLE_PITCH | FF_SWISS,  "Helvetica", "Helvetica",
          VARIABLE_PITCH | FF_ROMAN,  "Tms Rmn",   "Times Roman"
          } ;
     DWORD   dwSize ;
     HBITMAP hBitmap ;
     HDC     hDC, hMemDC ;
     HFONT   hFont ;
     LOGFONT lf ;

     hFont = GetStockObject (SYSTEM_FONT) ;
     GetObject (hFont, sizeof (LOGFONT), (LPSTR) &lf) ;

     lf.lfHeight *= 2 ;
     lf.lfWidth  *= 2 ;
     lf.lfPitchAndFamily = lfSet[i].lfPitchAndFamily ;
     strcpy (lf.lfFaceName, lfSet[i].lfFaceName) ;

     hDC = CreateIC ("DISPLAY", NULL, NULL, NULL) ;
     hMemDC = CreateCompatibleDC (hDC) ;
     SelectObject (hMemDC, CreateFontIndirect (&lf)) ;
     dwSize = GetTextExtent (hMemDC, lfSet[i].szMenuText,
                              strlen (lfSet[i].szMenuText)) ;

     hBitmap = CreateBitmap (LOWORD (dwSize), HIWORD (dwSize), 1, 1, NULL) ;
     SelectObject (hMemDC, hBitmap) ;
     TextOut (hMemDC, 0, 0, lfSet[i].szMenuText,
                              strlen (lfSet[i].szMenuText)) ;

     DeleteObject (SelectObject (hMemDC, hFont)) ;
     DeleteDC (hMemDC) ;
     DeleteDC (hDC) ;

     return hBitmap ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND     hWnd ;
     unsigned iMessage ;
     WORD     wParam ;
     LONG     lParam ;
     {
     HMENU    hMenu ;
     static   short nCurrentFont = IDM_COUR ;

     switch (iMessage)
          {
          case WM_CREATE:
               CheckMenuItem (GetMenu (hWnd), nCurrentFont, MF_CHECKED) ;
               break ;

          case WM_SYSCOMMAND:
               switch (wParam)
                    {
                    case IDM_HELP:
                         MessageBeep (0) ;
                         MessageBox (hWnd, "Help not yet implemented.",
                                   szAppName, MB_OK | MB_ICONEXCLAMATION) ;
                         break ;
                    default:
                         return DefWindowProc (hWnd, iMessage,
                                                       wParam, lParam) ;
                    }
               break ;

          case WM_COMMAND:
               switch (wParam)
                    {
                    case IDM_NEW:
                    case IDM_OPEN:
                    case IDM_SAVE:
                    case IDM_SAVEAS:

                    case IDM_UNDO:
                    case IDM_CUT:
                    case IDM_COPY:
                    case IDM_PASTE:
                    case IDM_CLEAR:

                         MessageBeep (0) ;
                         break ;

                    case IDM_COUR:
                    case IDM_HELV:
                    case IDM_TMSRMN:

                         hMenu = GetMenu (hWnd) ;
                         CheckMenuItem (hMenu, nCurrentFont, MF_UNCHECKED) ;
                         nCurrentFont = wParam ;
                         CheckMenuItem (hMenu, nCurrentFont, MF_CHECKED) ;
                         break ;
                    }
               break ;

          case WM_DESTROY:
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
