/* WINPRTSC.C -- Windows Print Screen Utility */

#include <windows.h>
#include <string.h>

#define WM_PRTSC WM_USER 

long  FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;
DWORD FAR PASCAL KeyboardHook (int, WORD, LONG) ;
int   PrintScreen () ;

char    szAppName [] = "WinPrtSc" ;
char    szSplMsg  [] = "WinPrtSc - Printing Screen" ;
FARPROC lpfnKeyHook, lpfnOldHook ;
HANDLE  hInst ;
HWND    hWnd ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szLoadMsg [] = "Ctrl-Shift-F9 prints screen." ;
     MSG         msg ;
     WNDCLASS    wndclass ;
                         
     if (!hPrevInstance)
          { 
          wndclass.style         = CS_VREDRAW | CS_HREDRAW ;
          wndclass.lpfnWndProc   = WndProc ;
          wndclass.cbClsExtra    = 0 ;
          wndclass.cbWndExtra    = 0 ;
          wndclass.hInstance     = hInstance ;
          wndclass.hIcon         = LoadIcon (hInstance, szAppName) ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
          wndclass.hbrBackground = GetStockObject (WHITE_BRUSH) ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;

          hInst = hInstance ;

          hWnd = CreateWindow (szAppName, szAppName,
                              WS_OVERLAPPEDWINDOW,
                              CW_USEDEFAULT, 0,
                              CW_USEDEFAULT, 0,
                              NULL, NULL, hInstance, NULL) ;

          ShowWindow (hWnd, SW_SHOWMINNOACTIVE) ;
          UpdateWindow (hWnd) ;

          lpfnKeyHook = MakeProcInstance ((FARPROC) KeyboardHook, hInstance) ;
          lpfnOldHook = SetWindowsHook (WH_KEYBOARD, lpfnKeyHook) ;
          }

     if (nCmdShow != SW_SHOWMINNOACTIVE)
          MessageBox (NULL, szLoadMsg, szAppName, MB_OK | MB_ICONASTERISK) ;

     if (hPrevInstance)
          return FALSE ;

     while (GetMessage (&msg, NULL, 0, 0))
          {
          TranslateMessage (&msg) ;
          DispatchMessage (&msg) ;
          }
     return msg.wParam ;
     }

DWORD FAR PASCAL KeyboardHook (iCode, wParam, lParam)
     int  iCode ;
     WORD wParam ;
     LONG lParam ;
     {
     if (iCode == HC_ACTION && wParam == VK_F9 &&
                               GetKeyState (VK_SHIFT)   < 0 &&
                               GetKeyState (VK_CONTROL) < 0)
          {
          if (HIWORD (lParam) & 0x8000)
               PostMessage (hWnd, WM_PRTSC, 0, 0L) ;

          return TRUE ;
          }
     else
          return DefHookProc (iCode, wParam, lParam, &lpfnOldHook) ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND        hWnd ;
     unsigned    iMessage ;
     WORD        wParam ;
     LONG        lParam ;
     {
     static BOOL bNowPrinting = FALSE ;
     static char *szErrMsg [] = { "Already printing screen.  Try later.",
                                  "Printer is not available.",
                                  "Printer can't print bitmaps.",
                                  "Could not create bitmap of screen.",
                                  "Error encountered during printing.",
                                  "Printing aborted by user." } ;
     int         nReturnCode ;     

     switch (iMessage)
          {
          case WM_PRTSC:

               if (bNowPrinting)
                    MessageBox (NULL, szErrMsg [0], szAppName,
                                             MB_OK | MB_ICONEXCLAMATION) ;
               else
                    {
                    bNowPrinting = TRUE ;

                    if (nReturnCode = PrintScreen ())

                         MessageBox (NULL, szErrMsg [nReturnCode], szAppName,
                                             MB_OK | MB_ICONEXCLAMATION) ;

                    bNowPrinting = FALSE ;
                    }
               break ;

          case WM_QUERYOPEN:
               break ;

          case WM_DESTROY :
               UnhookWindowsHook (WH_KEYBOARD, lpfnKeyHook) ;
               PostQuitMessage (0) ;
               break ;

          default :
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
