/* PRINT.C -- Common Routines for Print1, Print2, Print3, and Print4 */

#include <windows.h>
#include <string.h>

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;
BOOL PrintMyPage (HWND) ;

extern HANDLE hInst ;
extern char szAppName [] ;
extern char szCaption [] ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE   hInstance, hPrevInstance ;
     LPSTR    lpszCmdLine ;
     int      nCmdShow ;
     {
     HWND     hWnd ;
     MSG      msg ;
     WNDCLASS wndclass ;

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
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }
     hInst = hInstance ;

     hWnd = CreateWindow (szAppName, szCaption,
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

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND     hWnd ;
     unsigned iMessage ;
     WORD     wParam ;
     LONG     lParam ;
     {
     HMENU    hMenu ;

     switch (iMessage)
          {
          case WM_CREATE:
               hMenu = GetSystemMenu (hWnd, FALSE);
               ChangeMenu (hMenu, 0, NULL,     0, MF_APPEND | MF_SEPARATOR);
               ChangeMenu (hMenu, 0, "&Print", 1, MF_APPEND | MF_STRING);
               break ;

          case WM_SYSCOMMAND:
               if (wParam == 1)
                    {
                    if (PrintMyPage (hWnd))
                         MessageBox (hWnd, "Could not print page",
                              szAppName, MB_OK | MB_ICONEXCLAMATION) ;
                    break ;
                    }
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;

          case WM_DESTROY:
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
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

void PageGDICalls (hPrnDC, xPage, yPage)
     HDC         hPrnDC ;
     short       xPage, yPage ;
     {
     static char szTextStr [] = "Hello Printer!" ;
     DWORD       dwExtent ;
     POINT       ptExtent ;

     Rectangle (hPrnDC, 0, 0, xPage, yPage) ;

     MoveTo (hPrnDC, 0, 0) ;
     LineTo (hPrnDC, xPage, yPage) ;
     MoveTo (hPrnDC, xPage, 0) ;
     LineTo (hPrnDC, 0, yPage) ;

     SaveDC (hPrnDC) ;

     SetMapMode (hPrnDC, MM_ISOTROPIC) ;
     SetWindowExt   (hPrnDC, 1000, 1000) ;
     SetViewportExt (hPrnDC, xPage / 2, -yPage / 2) ;
     SetViewportOrg (hPrnDC, xPage / 2,  yPage / 2) ;

     Ellipse (hPrnDC, -500, 500, 500, -500) ;

     dwExtent = GetTextExtent (hPrnDC, szTextStr, sizeof szTextStr - 1) ;
     ptExtent = MAKEPOINT (dwExtent) ;
     TextOut (hPrnDC, -ptExtent.x / 2, ptExtent.y / 2, szTextStr,
                                                  sizeof szTextStr - 1) ;

     RestoreDC (hPrnDC, -1) ;
     }
