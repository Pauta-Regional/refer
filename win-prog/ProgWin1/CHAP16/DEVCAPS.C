/*  DEVCAPS.C -- Displays Device Capability Information (Version 2) */

#include <windows.h>
#include <stdio.h>
#include <string.h>
#include "winundoc.h"
#include "devcaps.h"

void DoBasicInfo (HDC, HDC, short, short) ;            /* in DEVCAPS2.C */
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

     hWnd = CreateWindow (szAppName, NULL,
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

void DoEscSupport (hDC, hIC, xChar, yChar)
     HDC    hDC, hIC ;
     short  xChar, yChar ;
     {
     static struct
          {
          char  *szEscCode ;
          short nEscCode ;
          } 
          esc [] =
          {
          "NEWFRAME",          NEWFRAME,
          "ABORTDOC",          ABORTDOC,
          "NEXTBAND",          NEXTBAND,
          "SETCOLORTABLE",     SETCOLORTABLE,
          "GETCOLORTABLE",     GETCOLORTABLE,
          "FLUSHOUTPUT",       FLUSHOUTPUT,
          "DRAFTMODE",         DRAFTMODE,
          "QUERYESCSUPPORT",   QUERYESCSUPPORT,
          "SETABORTPROC",      SETABORTPROC,
          "STARTDOC",          STARTDOC,
          "ENDDOC",            ENDDOC,
          "GETPHYSPAGESIZE",   GETPHYSPAGESIZE,
          "GETPRINTINGOFFSET", GETPRINTINGOFFSET,
          "GETSCALINGFACTOR",  GETSCALINGFACTOR } ;

     static char *szYesNo [] = { "Yes", "No" } ;
     char        szBuffer [32] ;
     POINT       pt ;
     short       n, nReturn ;

     TextOut (hDC, xChar, yChar, "Escape Support", 14) ;

     for (n = 0 ; n < sizeof esc / sizeof esc [0] ; n++)
          {
          nReturn = Escape (hIC, QUERYESCSUPPORT, 1,
                                   (LPSTR) & esc[n].nEscCode, NULL) ;
          TextOut (hDC, 6 * xChar, (n + 3) * yChar, szBuffer,
               sprintf (szBuffer, "%-24s %3s", esc[n].szEscCode,
                                   szYesNo [nReturn > 0 ? 0 : 1])) ;

          if (nReturn > 0 && esc[n].nEscCode >= GETPHYSPAGESIZE
                          && esc[n].nEscCode <= GETSCALINGFACTOR)
               {
               Escape (hIC, esc[n].nEscCode, 0, NULL, (LPSTR) &pt) ;
               TextOut (hDC, 36 * xChar, (n + 3) * yChar, szBuffer,
                    sprintf (szBuffer, "(%u,%u)", pt.x, pt.y)) ;
               }
          }
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND          hWnd ;
     unsigned      iMessage ;
     WORD          wParam ;
     LONG          lParam ;
     {
     static char   szAllDevices [4096], szDevice [32], szDriver [16],
                   szDriverFile [16], szWindowText [64] ;
     static HANDLE hLibrary ;
     static short  n, xChar, yChar, nCurrentDevice = IDM_SCREEN,
                                    nCurrentInfo   = IDM_BASIC ;
     char          *szOutput, *szPtr ;
     FARPROC       lpfnDM ;
     HDC           hDC, hIC ;
     HMENU         hMenu ;
     PAINTSTRUCT   ps ;
     TEXTMETRIC    tm ;

     switch (iMessage)
          {
          case WM_CREATE:
               hDC = GetDC (hWnd) ;
               GetTextMetrics (hDC, &tm) ;
               xChar = tm.tmAveCharWidth ;
               yChar = tm.tmHeight + tm.tmExternalLeading ;
               ReleaseDC (hWnd, hDC) ;

               lParam = NULL ;
                                                  /* fall through */
          case WM_WININICHANGE:

               if (lParam != NULL && lstrcmp ((LPSTR) lParam, "devices") != 0)
                    break ;

               hMenu = GetSubMenu (GetMenu (hWnd), 0) ;

               while (GetMenuItemCount (hMenu) > 1)
                    ChangeMenu (hMenu, 1, NULL, 0, MF_DELETE | MF_BYPOSITION) ;

               GetProfileString ("devices", NULL, "", szAllDevices,
                         sizeof szAllDevices) ;

               n = IDM_SCREEN + 1 ;
               szPtr = szAllDevices ;
               while (*szPtr)
                    {
                    ChangeMenu (hMenu, 0, szPtr, n, MF_APPEND |
                                   (n % 16 ? 0 : MF_MENUBARBREAK)) ;
                    n++ ;
                    szPtr += strlen (szPtr) + 1 ;
                    }
               ChangeMenu (hMenu, 0, NULL, 0, MF_APPEND) ;
               ChangeMenu (hMenu, 0, "Device Mode", IDM_DEVMODE, MF_APPEND) ;

               wParam = IDM_SCREEN ;
                                                  /* fall through */
          case WM_COMMAND:
               hMenu = GetMenu (hWnd) ;

               if (wParam < IDM_DEVMODE)          /* IDM_SCREEN & Printers */
                    {
                    CheckMenuItem (hMenu, nCurrentDevice, MF_UNCHECKED) ;
                    nCurrentDevice = wParam ;
                    CheckMenuItem (hMenu, nCurrentDevice, MF_CHECKED) ;
                    }
               else if (wParam == IDM_DEVMODE)
                    {
                    GetMenuString (hMenu, nCurrentDevice, szDevice,
                                        sizeof szDevice, MF_BYCOMMAND) ;
                    GetProfileString ("devices", szDevice, "",
                                        szDriver, sizeof szDriver) ;

                    szOutput = strtok (szDriver, ", ") ;
                    strcat (strcpy (szDriverFile, szDriver), ".DRV") ;

                    if (hLibrary >= 32)
                         FreeLibrary (hLibrary) ;

                    hLibrary = LoadLibrary (szDriverFile) ;
                    if (hLibrary >= 32)
                         {
                         lpfnDM = GetProcAddress (hLibrary, "DEVICEMODE") ;
                         (*lpfnDM) (hWnd, hLibrary, (LPSTR) szDevice,
                                                    (LPSTR) szOutput) ;
                         }
                    }
               else                               /* info menu items */
                    {
                    CheckMenuItem (hMenu, nCurrentInfo, MF_UNCHECKED) ;
                    nCurrentInfo = wParam ;
                    CheckMenuItem (hMenu, nCurrentInfo, MF_CHECKED) ;
                    }
               InvalidateRect (hWnd, NULL, TRUE) ;
               break ;

          case WM_INITMENUPOPUP:
               if (lParam == 0)
                    EnableMenuItem (GetMenu (hWnd), IDM_DEVMODE,
                         nCurrentDevice == IDM_SCREEN ?
                              MF_GRAYED : MF_ENABLED) ;
               break ;

          case WM_PAINT:
               strcpy (szWindowText, "Device Capabilities: ") ;
          
               if (nCurrentDevice == IDM_SCREEN)
                    {
                    strcpy (szDriver, "DISPLAY") ;
                    strcat (szWindowText, szDriver) ;
                    hIC = CreateIC (szDriver, NULL, NULL, NULL) ;
                    }
               else
                    {
                    hMenu = GetMenu (hWnd) ;

                    GetMenuString (hMenu, nCurrentDevice, szDevice,
                                        sizeof szDevice, MF_BYCOMMAND) ;
                    GetProfileString ("devices", szDevice, "",
                                             szDriver, 10) ;
                    szOutput = strtok (szDriver, ", ") ;
                    strcat (szWindowText, szDevice) ;
                    
                    hIC = CreateIC (szDriver, szDevice, szOutput, NULL) ;
                    }
               SetWindowText (hWnd, szWindowText) ;

               hDC = BeginPaint (hWnd, &ps) ;

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

                         case IDM_ESC:
                              DoEscSupport (hDC, hIC, xChar, yChar) ;
                              break ;
                         }
                    DeleteDC (hIC) ;
                    }

               EndPaint (hWnd, &ps) ;
               break ;

          case WM_DESTROY:
               if (hLibrary >= 32)
                    FreeLibrary (hLibrary) ;

               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
