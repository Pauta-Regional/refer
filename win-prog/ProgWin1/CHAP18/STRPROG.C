/* STRPROG.C -- Program Using STRLIB Library */

#include <windows.h>
#include <string.h>
#include "winundoc.h"
#include "strprog.h"

#define MAXLEN 32
#define WM_DATACHANGE WM_USER

typedef struct
     {
     HDC   hDC ;
     short xText ;
     short yText ;
     short xStart ;
     short yStart ;
     short xIncr ;
     short yIncr ;
     short xMax ;
     short yMax ;
     }
     CBPARM ;

BOOL  FAR PASCAL AddString    (LPSTR) ;      /* functions in STRLIB */
BOOL  FAR PASCAL DeleteString (LPSTR) ;
short FAR PASCAL GetStrings   (FARPROC, CBPARM FAR *) ;

long  FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

char  szAppName [] = "StrProg" ;
char  szString  [MAXLEN] ;

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
          wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
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

     hWnd = CreateWindow (szAppName, "Library Demonstration Program",
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

BOOL FAR PASCAL DlgProc (hDlg, iMessage, wParam, lParam)
     HWND     hDlg ;
     unsigned iMessage ;
     WORD     wParam ;
     LONG     lParam ;
     {
     switch (iMessage)
          {
          case WM_INITDIALOG:
               SendDlgItemMessage (hDlg, IDD_STRING, EM_LIMITTEXT,
                                                       MAXLEN - 1, 0L);
               break ;

          case WM_COMMAND:
               switch (wParam)
                    {
                    case IDOK:
                         GetDlgItemText (hDlg, IDD_STRING, szString, MAXLEN) ;
                         EndDialog (hDlg, TRUE) ;
                         break ;

                    case IDCANCEL:
                         EndDialog (hDlg, FALSE) ;
                         break ;

                    default:
                         return FALSE ;
                    }
          default:
               return FALSE ;
          }
     return TRUE ;
     }

BOOL FAR PASCAL EnumCallBack (hWnd, lParam)
     HWND hWnd ;
     LONG lParam ;
     {
     char szClassName [16] ;

     GetClassName (hWnd, szClassName, sizeof szClassName) ;
     if (0 == strcmp (szClassName, szAppName))
          SendMessage (hWnd, WM_DATACHANGE, 0, 0L) ;

     return TRUE ;
     }

BOOL FAR PASCAL GetStrCallBack (lpString, lpcbp)
     LPSTR       lpString ;
     CBPARM FAR *lpcbp ;
     {
     TextOut (lpcbp->hDC, lpcbp->xText, lpcbp->yText, lpString,
                                             lstrlen (lpString)) ;

     if ((lpcbp->yText += lpcbp->yIncr) > lpcbp->yMax)
          {
          lpcbp->yText = lpcbp->yStart ;
          if ((lpcbp->xText += lpcbp->xIncr) > lpcbp->xMax)
               return FALSE ;
          }
     return TRUE ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND           hWnd ;
     unsigned       iMessage ;
     WORD           wParam ;
     LONG           lParam ;
     {
     static FARPROC lpfnDlgProc, lpfnGetStrCallBack, lpfnEnumCallBack ;
     static HANDLE  hInstance ;
     static short   xChar, yChar, xClient, yClient ;
     CBPARM         cbparam ;
     HDC            hDC ;
     PAINTSTRUCT    ps ;
     TEXTMETRIC     tm ;

     switch (iMessage)
          {
          case WM_CREATE:
               hInstance = ((LPCREATESTRUCT) lParam)->hInstance ;

               lpfnDlgProc = MakeProcInstance (DlgProc, hInstance) ;
               lpfnGetStrCallBack = MakeProcInstance (GetStrCallBack,
                                                            hInstance) ;
               lpfnEnumCallBack = MakeProcInstance (EnumCallBack, hInstance) ;

               hDC = GetDC (hWnd) ;
               GetTextMetrics (hDC, &tm) ;
               xChar = tm.tmAveCharWidth ;
               yChar = tm.tmHeight + tm.tmExternalLeading ;
               ReleaseDC (hWnd, hDC) ;

               break ;

          case WM_COMMAND:
               switch (wParam)
                    {
                    case IDM_ENTER:

                         if (DialogBox (hInstance, "EnterDlg", hWnd,
                                             lpfnDlgProc))
                              {
                              if (AddString (szString))
                                   EnumWindows (lpfnEnumCallBack, 0L) ;
                              else
                                   MessageBeep (0) ;
                              }
                         break ;

                    case IDM_DELETE:

                         if (DialogBox (hInstance, "DeleteDlg", hWnd,
                                             lpfnDlgProc))
                              {
                              if (DeleteString (szString))
                                   EnumWindows (lpfnEnumCallBack, 0L) ;
                              else
                                   MessageBeep (0) ;
                              }
                         break ;
                    }
               break ;

          case WM_SIZE:
               xClient = LOWORD (lParam) ;
               yClient = HIWORD (lParam) ;
               break ;

          case WM_DATACHANGE:
               InvalidateRect (hWnd, NULL, TRUE) ;
               break ;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;

               cbparam.hDC   = hDC ;
               cbparam.xText = cbparam.xStart = xChar ;
               cbparam.yText = cbparam.yStart = yChar ;
               cbparam.xIncr = xChar * MAXLEN ;
               cbparam.yIncr = yChar ;
               cbparam.xMax  = cbparam.xIncr * (1 + xClient / cbparam.xIncr) ;
               cbparam.yMax  = yChar * (yClient / yChar - 1) ;

               GetStrings (lpfnGetStrCallBack, &cbparam) ;

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
