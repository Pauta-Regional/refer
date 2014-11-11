/* ABOUT3.C -- About Box Demo Program No. 3 */

#include <windows.h>

#define IDM_ABOUT 1

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;
long FAR PASCAL EllipPushWndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance;
     LPSTR       lpszCmdLine;
     int         nCmdShow;
     {
     static char szAppName [] = "About3" ;
     MSG         msg;
     HWND        hWnd ;
     WNDCLASS    wndclass ;

     if (!hPrevInstance) 
          {
          wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
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
               return FALSE;

          wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
          wndclass.lpfnWndProc   = EllipPushWndProc ;
          wndclass.cbClsExtra    = 0 ;
          wndclass.cbWndExtra    = 0 ;
          wndclass.hInstance     = hInstance ;
          wndclass.hIcon         = NULL ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
          wndclass.hbrBackground = COLOR_WINDOW + 1 ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = "EllipPush" ;

          if (!RegisterClass (&wndclass))
               return FALSE;
          }

     hWnd = CreateWindow (szAppName, "About Box Demo Program",
                         WS_OVERLAPPEDWINDOW,
                         CW_USEDEFAULT, 0,
                         CW_USEDEFAULT, 0,
                         NULL, NULL, hInstance, NULL) ;

     ShowWindow (hWnd, nCmdShow) ;
     UpdateWindow (hWnd); 

     while (GetMessage (&msg, NULL, 0, 0))
          {
          TranslateMessage (&msg) ;
          DispatchMessage (&msg) ;
          }
     return msg.wParam ;
     }

BOOL FAR PASCAL AboutDlgProc (hDlg, iMessage, wParam, lParam)
     HWND     hDlg ;
     unsigned iMessage ;
     WORD     wParam ;
     LONG     lParam ;
     {
     switch (iMessage)
          {
          case WM_INITDIALOG:
               break ;

          case WM_COMMAND:
               switch (wParam)
                    {
                    case IDOK:
                         EndDialog (hDlg, 0) ;
                         break ;

                    default:
                         return FALSE ;
                    }
               break ;

          default:
               return FALSE ;
          }
     return TRUE ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND           hWnd;
     unsigned       iMessage;
     WORD           wParam;
     LONG           lParam;
     {
     static FARPROC lpfnAboutDlgProc ;
     static HWND    hInstance ;
     HMENU          hMenu ;

     switch (iMessage)
          {
          case WM_CREATE:
               hInstance = ((LPCREATESTRUCT) lParam)->hInstance ;

               lpfnAboutDlgProc = MakeProcInstance (AboutDlgProc, hInstance) ;

               hMenu = GetSystemMenu (hWnd, FALSE) ;
               ChangeMenu (hMenu, 0, NULL, 0, MF_APPEND) ;
               ChangeMenu (hMenu, 0, "A&bout About3...", IDM_ABOUT, MF_APPEND);
               break ;

          case WM_SYSCOMMAND:

               switch (wParam)
                    {
                    case IDM_ABOUT:
                         DialogBox (hInstance, "AboutBox", hWnd,
                                        lpfnAboutDlgProc) ;
                         break ;
                    }
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
     
          case WM_DESTROY :
               PostQuitMessage (0) ;
               break ;

          default :
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }

long FAR PASCAL EllipPushWndProc (hWnd, iMessage, wParam, lParam)
     HWND        hWnd;
     unsigned    iMessage;
     WORD        wParam;
     LONG        lParam;
     {
     char        szText [40] ;
     HBRUSH      hBrush ;
     HDC         hDC ;
     PAINTSTRUCT ps ;
     RECT        rect ;

     switch (iMessage)
          {
          case WM_PAINT:
               GetClientRect (hWnd, &rect) ;
               GetWindowText (hWnd, szText, sizeof szText) ;

               hDC = BeginPaint (hWnd, &ps) ;

               hBrush = CreateSolidBrush (GetSysColor (COLOR_WINDOW)) ;
               hBrush = SelectObject (hDC, hBrush) ;
               SetBkColor (hDC, GetSysColor (COLOR_WINDOW)) ;
               SetTextColor (hDC, GetSysColor (COLOR_WINDOWTEXT)) ;

               Ellipse (hDC, rect.left, rect.top, rect.right, rect.bottom) ;
               DrawText (hDC, szText, -1, &rect,
                              DT_SINGLELINE | DT_CENTER | DT_VCENTER) ;

               DeleteObject (SelectObject (hDC, hBrush)) ;

               EndPaint (hWnd, &ps) ;
               break ;

          case WM_KEYUP:
               if (wParam != VK_SPACE)
                    break ;
                                        /* fall through */
          case WM_LBUTTONUP:
               SendMessage (GetParent (hWnd), WM_COMMAND,
                    GetWindowWord (hWnd, GWW_ID), (LONG) hWnd) ;
               break ;

          default :
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
