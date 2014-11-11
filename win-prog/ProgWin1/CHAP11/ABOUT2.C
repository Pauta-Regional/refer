/* ABOUT2.C -- About Box Demo Program No. 2 */

#include <windows.h>
#include "about2.h"

#define  IDM_ABOUT 1

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG);

short nCurrentColor = IDD_BLACK, nCurrentFigure = IDD_RECT ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance;
     LPSTR       lpszCmdLine;
     int         nCmdShow;
     {
     static char szAppName [] = "About2" ;
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

void PaintWindow (hWnd, nColor, nFigure)
     HWND         hWnd ;
     short        nColor, nFigure ;
     {
     static DWORD dwColor [8] = { RGB (0,     0, 0), RGB (  0,   0, 255),
                                  RGB (0,   255, 0), RGB (  0, 255, 255),
                                  RGB (255,   0, 0), RGB (255,   0, 255),
                                  RGB (255, 255, 0), RGB (255, 255, 255) } ;
     HBRUSH       hBrush ;
     HDC          hDC ;
     RECT         rect ;

     hDC = GetDC (hWnd) ;
     GetClientRect (hWnd, &rect) ;
     hBrush = CreateSolidBrush (dwColor [nColor - IDD_BLACK]) ;
     hBrush = SelectObject (hDC, hBrush) ;

     if (nFigure == IDD_RECT)
          Rectangle (hDC, rect.left, rect.top, rect.right, rect.bottom) ;
     else
          Ellipse   (hDC, rect.left, rect.top, rect.right, rect.bottom) ;

     DeleteObject (SelectObject (hDC, hBrush)) ;
     ReleaseDC (hWnd, hDC) ;
     }

void PaintTheBlock (hCtrl, nColor, nFigure)
     HWND  hCtrl ;
     short nColor, nFigure ;
     {
     InvalidateRect (hCtrl, NULL, TRUE) ;
     UpdateWindow (hCtrl) ;
     PaintWindow (hCtrl, nColor, nFigure) ;
     }

BOOL FAR PASCAL AboutDlgProc (hDlg, iMessage, wParam, lParam)
     HWND         hDlg ;
     unsigned     iMessage ;
     WORD         wParam ;
     LONG         lParam ;
     {
     static HWND  hCtrlBlock ;
     static short nColor, nFigure ;

     switch (iMessage)
          {
          case WM_INITDIALOG:
               nColor  = nCurrentColor ;
               nFigure = nCurrentFigure ;

               CheckRadioButton (hDlg, IDD_BLACK, IDD_WHITE, nColor) ; 
               CheckRadioButton (hDlg, IDD_RECT,  IDD_ELL,   nFigure) ;

               hCtrlBlock = GetDlgItem (hDlg, IDD_PAINT) ;

               SetFocus (GetDlgItem (hDlg, nColor)) ;
               return FALSE ;

          case WM_COMMAND:
               switch (wParam)
                    {
                    case IDOK:
                         nCurrentColor  = nColor ;
                         nCurrentFigure = nFigure ;
                         EndDialog (hDlg, TRUE) ;
                         break ;

                    case IDCANCEL:
                         EndDialog (hDlg, FALSE) ;
                         break ;

                    case IDD_BLACK:
                    case IDD_RED:
                    case IDD_GREEN:
                    case IDD_YELLOW:
                    case IDD_BLUE:
                    case IDD_MAGENTA:
                    case IDD_CYAN:
                    case IDD_WHITE:

                         nColor = wParam ;
                         CheckRadioButton (hDlg, IDD_BLACK, IDD_WHITE, wParam);
                         PaintTheBlock (hCtrlBlock, nColor, nFigure) ;
                         break ;

                    case IDD_RECT:
                    case IDD_ELL:

                         nFigure = wParam ;
                         CheckRadioButton (hDlg, IDD_RECT, IDD_ELL, wParam) ;
                         PaintTheBlock (hCtrlBlock, nColor, nFigure) ;
                         break ;

                    default:
                         return FALSE ;
                    }
               break ;

          case WM_PAINT:

               PaintTheBlock (hCtrlBlock, nColor, nFigure) ;
               return FALSE ;

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
     PAINTSTRUCT    ps ;

     switch (iMessage)
          {
          case WM_CREATE:
               hInstance = ((LPCREATESTRUCT) lParam)->hInstance ;

               lpfnAboutDlgProc = MakeProcInstance (AboutDlgProc, hInstance) ;

               hMenu = GetSystemMenu (hWnd, FALSE) ;
               ChangeMenu (hMenu, 0, NULL, 0, MF_APPEND) ;
               ChangeMenu (hMenu, 0, "A&bout About2...", IDM_ABOUT, MF_APPEND);
               break ;

          case WM_SYSCOMMAND:

               switch (wParam)
                    {
                    case IDM_ABOUT:
                         if (DialogBox (hInstance, "AboutBox", hWnd,
                                        lpfnAboutDlgProc))
                              InvalidateRect (hWnd, NULL, TRUE) ;
                         break ;
                    }
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;

          case WM_PAINT:
               BeginPaint (hWnd, &ps) ;
               EndPaint (hWnd, &ps) ;

               PaintWindow (hWnd, nCurrentColor, nCurrentFigure) ;
               break ;               
                    
          case WM_DESTROY :
               PostQuitMessage (0) ;
               break ;

          default :
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
