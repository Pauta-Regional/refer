/*  DEVCAPS.C -- Displays Device Capability Information (Version 1) */

#include <windows.h>
#include <string.h>
#include "devcaps.h"

void DoBasicInfo (HDC, HDC, short, short) ;       /* in DEVCAPS2.C */
void DoOtherInfo (HDC, HDC, short, short) ;
void DoBitCodedCaps (HDC, HDC, short, short, short) ;

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "DevCaps" ;
     HWND        hWnd ;
     MSG         msg ;
     WNDCLASS    wndclass ;

     if (!hPrevInstance) 
          {
          wndclass.style         = CS_HREDRAW | CS_VREDRAW;
          wndclass.lpfnWndProc   = WndProc ;
          wndclass.cbClsExtra    = 0 ;
          wndclass.cbWndExtra    = 0 ;
          wndclass.hInstance     = hInstance ;
          wndclass.hIcon         = LoadIcon (NULL, IDI_APPLICATION) ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
          wndclass.hbrBackground = GetStockObject (WHITE_BRUSH) ;
          wndclass.lpszMenuName  = szAppName ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }

     hWnd = CreateWindow (szAppName, "Device Capabilities",
                         WS_OVERLAPPEDWINDOW,
                         CW_USEDEFAULT, 0,
                         CW_USEDEFAULT, 0,
                         NULL, NULL, hInstance, NULL) ;

     ShowWindow (hWnd, nCmdShow) ;
     UpdateWindow (hWnd) ;

     while (GetMessage (&msg, NULL, 0, 0))
          {
          TranslateMessage (&msg) ;
          DispatchMessage (&msg) ;
          }
     return msg.wParam ;
     }

HDC GetPrinterIC ()
     {
     char szPrinter [64] ;
     char *szDevice, *szDriver, *szOutput ;

     GetProfileString ("windows", "device", "", szPrinter, 64) ;

     if ((szDevice = strtok (szPrinter, "," )) &&
         (szDriver = strtok (NULL,      ", ")) && 
         (szOutput = strtok (NULL,      ", ")))
          
               return CreateIC (szDriver, szDevice, szOutput, NULL) ;

     return NULL ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND         hWnd ;
     unsigned     iMessage ;
     WORD         wParam ;
     LONG         lParam ;
     {
     static short xChar, yChar, nCurrentDevice = IDM_SCREEN,
                                nCurrentInfo   = IDM_BASIC ;
     HDC          hDC, hIC ;
     HMENU        hMenu ;
     PAINTSTRUCT  ps ;
     TEXTMETRIC   tm ;

     switch (iMessage)
          {
          case WM_CREATE:
               hDC = GetDC (hWnd) ;
               GetTextMetrics (hDC, &tm) ;
               xChar = tm.tmAveCharWidth ;
               yChar = tm.tmHeight + tm.tmExternalLeading ;
               ReleaseDC (hWnd, hDC) ;
               break ;

          case WM_COMMAND:
               hMenu = GetMenu (hWnd) ;

               switch (wParam)
                    {
                    case IDM_SCREEN:
                    case IDM_PRINTER:
                         CheckMenuItem (hMenu, nCurrentDevice, MF_UNCHECKED) ;
                         nCurrentDevice = wParam ;
                         CheckMenuItem (hMenu, nCurrentDevice, MF_CHECKED) ;
                         InvalidateRect (hWnd, NULL, TRUE) ;
                         break ;

                    case IDM_BASIC:
                    case IDM_OTHER:
                    case IDM_CURVE:
                    case IDM_LINE:
                    case IDM_POLY:
                    case IDM_TEXT:
                         CheckMenuItem (hMenu, nCurrentInfo, MF_UNCHECKED) ;
                         nCurrentInfo = wParam ;
                         CheckMenuItem (hMenu, nCurrentInfo, MF_CHECKED) ;
                         InvalidateRect (hWnd, NULL, TRUE) ;
                         break ;
                    }
               break ;

          case WM_DEVMODECHANGE:
               InvalidateRect (hWnd, NULL, TRUE) ;
               break ;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;
          
               if (nCurrentDevice == IDM_SCREEN)
                    hIC = CreateIC ("DISPLAY", NULL, NULL, NULL) ;
               else
                    hIC = GetPrinterIC () ;

               if (hIC)
                    {
                    switch (nCurrentInfo)
                         {
                         case IDM_BASIC:
                              DoBasicInfo (hDC, hIC, xChar, yChar) ;
                              break ;

                         case IDM_OTHER:
                              DoOtherInfo (hDC, hIC, xChar, yChar) ;
                              break ;

                         case IDM_CURVE:
                         case IDM_LINE:
                         case IDM_POLY:
                         case IDM_TEXT:
                              DoBitCodedCaps (hDC, hIC, xChar, yChar,
                                   nCurrentInfo - IDM_CURVE) ;
                              break ;
                         }
                    DeleteDC (hIC) ;
                    }

               EndPaint (hWnd, &ps) ;
               break ;

          case WM_DESTROY:
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
