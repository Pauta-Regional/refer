/* COLORSCR.C -- Modeless Dialog Box Version */

#include <windows.h>

long FAR PASCAL WndProc     (HWND, unsigned, WORD, LONG) ;
BOOL FAR PASCAL ColorScrDlg (HWND, unsigned, WORD, LONG) ;

HWND  hDlgModeless ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance;
     LPSTR       lpszCmdLine;
     int         nCmdShow;
     {
     static char szAppName[] = "ColorScr" ;
     HWND        hWnd ;
     MSG         msg;
     WNDCLASS    wndclass ;

     if (hPrevInstance)
          return FALSE ;

     wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
     wndclass.lpfnWndProc   = WndProc ;
     wndclass.cbClsExtra    = 0 ;
     wndclass.cbWndExtra    = 0 ;
     wndclass.hInstance     = hInstance ;
     wndclass.hIcon         = NULL ;
     wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
     wndclass.hbrBackground = CreateSolidBrush (0L) ;
     wndclass.lpszMenuName  = NULL ;
     wndclass.lpszClassName = szAppName ;

     if (!RegisterClass (&wndclass))
          return FALSE;

     hWnd = CreateWindow (szAppName, "Color Scroll",
                         WS_OVERLAPPEDWINDOW | WS_CLIPCHILDREN,
                         CW_USEDEFAULT, 0,
                         CW_USEDEFAULT, 0,
                         NULL, NULL, hInstance, NULL) ;

     ShowWindow (hWnd, nCmdShow) ;
     UpdateWindow (hWnd);

     hDlgModeless = CreateDialog (hInstance, "ColorScrDlg", hWnd,
                         MakeProcInstance (ColorScrDlg, hInstance)) ;

     while (GetMessage (&msg, NULL, 0, 0))
          {
          if (hDlgModeless == 0 || !IsDialogMessage (hDlgModeless, &msg))
               {
               TranslateMessage (&msg) ;
               DispatchMessage  (&msg) ;
               }
          }
     return msg.wParam ;
     }

BOOL FAR PASCAL ColorScrDlg (hDlg, iMessage, wParam, lParam)
     HWND         hDlg ;
     unsigned     iMessage ;
     WORD         wParam ;
     LONG         lParam ;
     {
     static short color [3] ;
     HWND         hWndParent, hCtrl ;
     short        nCtrlID, nIndex ;

     switch (iMessage)
          {
          case WM_INITDIALOG:
               for (nCtrlID = 10 ; nCtrlID < 13 ; nCtrlID++)
                    {
                    hCtrl = GetDlgItem (hDlg, nCtrlID) ;
                    SetScrollRange (hCtrl, SB_CTL, 0, 255, FALSE) ;
                    SetScrollPos   (hCtrl, SB_CTL, 0, FALSE) ;
                    }
               break ;

          case WM_VSCROLL :
               hCtrl   = HIWORD (lParam) ;
               nCtrlID = GetWindowWord (hCtrl, GWW_ID) ;
               nIndex  = nCtrlID - 10 ;
               hWndParent = GetParent (hDlg) ;

               switch (wParam)
                    {
                    case SB_PAGEDOWN :
                         color [nIndex] += 15 ;        /* fall through */
                    case SB_LINEDOWN :
                         color [nIndex] = min (255, color [nIndex] + 1) ;
                         break ;
                    case SB_PAGEUP :
                         color [nIndex] -= 15 ;        /* fall through */
                    case SB_LINEUP :
                         color [nIndex] = max (0, color [nIndex] - 1) ;
                         break ;
                    case SB_TOP:
                         color [nIndex] = 0 ;
                         break ;
                    case SB_BOTTOM :
                         color [nIndex] = 255 ;
                         break ;
                    case SB_THUMBPOSITION :
                    case SB_THUMBTRACK :
                         color [nIndex] = LOWORD (lParam) ;
                         break ;
                    default :
                         return FALSE ;
                    }
               SetScrollPos  (hCtrl, SB_CTL,      color [nIndex], TRUE) ;
               SetDlgItemInt (hDlg,  nCtrlID + 3, color [nIndex], FALSE) ;

               DeleteObject (GetClassWord (hWndParent, GCW_HBRBACKGROUND)) ;
               SetClassWord (hWndParent, GCW_HBRBACKGROUND,
                    CreateSolidBrush (RGB (color [0], color [1], color [2]))) ;

               InvalidateRect (hWndParent, NULL, TRUE) ;
               break ;

          default:
               return FALSE ;
          }
     return TRUE ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND     hWnd;
     unsigned iMessage;
     WORD     wParam;
     LONG     lParam;
     {
     switch (iMessage)
          {
          case WM_DESTROY:
               DeleteObject (GetClassWord (hWnd, GCW_HBRBACKGROUND)) ;
               PostQuitMessage (0) ;
               break ;

          default :
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
