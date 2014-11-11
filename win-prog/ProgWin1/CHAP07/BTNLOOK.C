/* BTNLOOK.C -- Button Look Program */

#include <windows.h>
#include <stdio.h>

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE   hInstance, hPrevInstance ;
     LPSTR    lpszCmdLine ;
     int      nCmdShow ;
     {
     WNDCLASS wndclass ;
     HWND     hWnd ;
     MSG      msg ;
     static   char szAppName [] = "BtnLook" ;

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

     hWnd = CreateWindow (szAppName, "Button Look",
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

struct
     {
     long style ;
     char *text ;
     }
     button [] =
     {
     BS_PUSHBUTTON,      "PUSHBUTTON",
     BS_DEFPUSHBUTTON,   "DEFPUSHBUTTON",
     BS_CHECKBOX,        "CHECKBOX", 
     BS_AUTOCHECKBOX,    "AUTOCHECKBOX",
     BS_RADIOBUTTON,     "RADIOBUTTON",
     BS_3STATE,          "3STATE",
     BS_AUTO3STATE,      "AUTO3STATE",
     BS_GROUPBOX,        "GROUPBOX",
     BS_USERBUTTON,      "USERBUTTON",
     BS_AUTORADIOBUTTON, "AUTORADIO",
     BS_PUSHBOX,         "PUSHBOX"
     } ;

#define NUM (sizeof button / sizeof button [0])

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND         hWnd ;
     unsigned     iMessage ;
     WORD         wParam ;
     LONG         lParam ;
     {
     static char  szPrm []    = "wParam       LOWORD(lParam)  HIWORD(lParam)",
                  szTop []    = "Control ID   Window Handle   Notification",
                  szUnd []    = "__________   _____________   ____________", 
                  szFormat [] = " %5u           %4X          %5u",
                  szBuffer [50] ;
     static HWND  hWndButton [NUM] ;
     static RECT  rect ;
     static short xChar, yChar ;
     HDC          hDC ;
     PAINTSTRUCT  ps ;
     short        i ;
     TEXTMETRIC   tm ;

     switch (iMessage)
          {
          case WM_CREATE:
               hDC = GetDC (hWnd) ;
               GetTextMetrics (hDC, &tm) ;
               xChar = tm.tmAveCharWidth ;
               yChar = tm.tmHeight + tm.tmExternalLeading ;
               ReleaseDC (hWnd, hDC) ;

               for (i = 0 ; i < NUM ; i++)
                    hWndButton [i] = CreateWindow ("button", button[i].text,
                              WS_CHILD | WS_VISIBLE | button[i].style,
                              xChar, yChar * (1 + 2 * i),
                              16 * xChar, 2 * yChar,
                              hWnd, i,
                              ((LPCREATESTRUCT) lParam) -> hInstance, NULL) ;
               break ;

          case WM_SIZE:
               rect.left   = 20 * xChar ;
               rect.top    = 3 * yChar ;
               rect.right  = LOWORD (lParam) ;
               rect.bottom = HIWORD (lParam) ;
               break ;

          case WM_PAINT:
               InvalidateRect (hWnd, &rect, TRUE) ;
               hDC = BeginPaint (hWnd, &ps) ;
               SetBkMode (hDC, TRANSPARENT) ;
               TextOut (hDC, 20 * xChar, 1 * yChar, szPrm, sizeof szPrm - 1) ;
               TextOut (hDC, 20 * xChar, 2 * yChar, szTop, sizeof szTop - 1) ;
               TextOut (hDC, 20 * xChar, 2 * yChar, szUnd, sizeof szUnd - 1) ;
               EndPaint (hWnd, &ps) ;
               break ;

          case WM_COMMAND:
               ScrollWindow (hWnd, 0, -yChar, &rect, &rect) ;
               hDC = GetDC (hWnd) ;

               TextOut (hDC, 20 * xChar, yChar * (rect.bottom / yChar - 1),
                         szBuffer, sprintf (szBuffer, szFormat, wParam,
                              LOWORD (lParam), HIWORD (lParam))) ;

               ReleaseDC (hWnd, hDC) ;
               ValidateRect (hWnd, NULL) ;
               break ;

          case WM_DESTROY:
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
