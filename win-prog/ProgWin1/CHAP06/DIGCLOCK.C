/*  DIGCLOCK.C -- Digital Clock Program */

#include <windows.h>
#include <stdio.h>
#include <time.h>

#define YEAR  (datetime->tm_year % 100)
#define MONTH (datetime->tm_mon  + 1)
#define MDAY  (datetime->tm_mday)
#define WDAY  (datetime->tm_wday)
#define HOUR  (datetime->tm_hour)
#define MIN   (datetime->tm_min)
#define SEC   (datetime->tm_sec)

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG);
void SizeTheWindow (short *, short *, short *, short *) ;

char  sDate [2], sTime [2], sAMPM [2][5] ;
int   iDate, iTime ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance;
     LPSTR       lpszCmdLine;
     int         nCmdShow;
     {
     static char szAppName [] = "DigClock" ;
     HWND        hWnd;
     MSG         msg;
     short       xStart, yStart, xClient, yClient ;
     WNDCLASS    wndclass ;

     if (!hPrevInstance)
          {
          wndclass.style         = CS_HREDRAW | CS_VREDRAW;
          wndclass.lpfnWndProc   = WndProc ;
          wndclass.cbClsExtra    = 0 ;
          wndclass.cbWndExtra    = 0 ;
          wndclass.hInstance     = hInstance ;
          wndclass.hIcon         = NULL ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
          wndclass.hbrBackground = GetStockObject (WHITE_BRUSH) ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE;
          }

     SizeTheWindow (&xStart, &yStart, &xClient, &yClient) ;

     hWnd = CreateWindow (szAppName, szAppName,
                         WS_POPUP | WS_DLGFRAME | WS_SYSMENU,
                         xStart,  yStart,
                         xClient, yClient,
                         NULL, NULL, hInstance, NULL) ;

     if (!SetTimer (hWnd, 1, 1000, NULL))
          {
          MessageBox (hWnd, "Too many clocks or timers!", szAppName,
                         MB_ICONEXCLAMATION | MB_OK) ;
          return FALSE ;
          }

     ShowWindow (hWnd, SW_SHOWNOACTIVATE) ; 
     UpdateWindow (hWnd);

     while (GetMessage (&msg, NULL, 0, 0))
          {
          TranslateMessage (&msg) ;
          DispatchMessage (&msg) ;
          }
     return msg.wParam;
     }

void SizeTheWindow (pxStart, pyStart, pxClient, pyClient)
     short      *pxStart, *pyStart, *pxClient, *pyClient ;
     {
     HDC        hDC ;
     TEXTMETRIC tm ;

     hDC = CreateIC ("DISPLAY", NULL, NULL, NULL) ;
     GetTextMetrics (hDC, &tm) ;
     DeleteDC (hDC) ;

     *pxClient = 2 * GetSystemMetrics (SM_CXDLGFRAME) + 16*tm.tmAveCharWidth ;
     *pxStart  =     GetSystemMetrics (SM_CXSCREEN)   - *pxClient ;
     *pyClient = 2 * GetSystemMetrics (SM_CYDLGFRAME) + 2*tm.tmHeight ;
     *pyStart  =     GetSystemMetrics (SM_CYSCREEN)   - *pyClient ;
     }

void SetInternational ()
     {
     static char cName [] = "intl" ;

     iDate = GetProfileInt (cName, "iDate", 0) ;
     iTime = GetProfileInt (cName, "iTime", 0) ;

     GetProfileString (cName, "sDate",  "/", sDate,     2) ;
     GetProfileString (cName, "sTime",  ":", sTime,     2) ;
     GetProfileString (cName, "s1159", "AM", sAMPM [0], 5) ;
     GetProfileString (cName, "s2359", "PM", sAMPM [1], 5) ;
     }

void WndPaint (hWnd, hDC)
     HWND        hWnd ;
     HDC         hDC ;
     {
     static char szWday [] = "Sun\0Mon\0Tue\0Wed\0Thu\0Fri\0Sat" ;
     char        cBuffer [40] ;
     long        lTime ;
     RECT        rect ;
     short       nLength ;
     struct tm   *datetime ;

     time (&lTime) ;
     datetime = localtime (&lTime) ;
     
     nLength = sprintf (cBuffer, " %s  %d%s%02d%s%02d \r\n", szWday + 4 * WDAY,
               iDate == 1 ? MDAY  : iDate == 2 ? YEAR  : MONTH, sDate,
               iDate == 1 ? MONTH : iDate == 2 ? MONTH : MDAY,  sDate,
               iDate == 1 ? YEAR  : iDate == 2 ? MDAY  : YEAR) ;

     if (iTime == 1)
          nLength += sprintf (cBuffer + nLength, " %02d%s%02d%s%02d ",
                         HOUR, sTime, MIN, sTime, SEC) ;
     else
          nLength += sprintf (cBuffer + nLength, " %d%s%02d%s%02d %s ",
                         (HOUR % 12) ? (HOUR % 12) : 12,
                         sTime, MIN, sTime, SEC, sAMPM [HOUR / 12]) ;

     GetClientRect (hWnd, &rect) ;
     DrawText (hDC, cBuffer, -1, &rect, DT_CENTER | DT_NOCLIP) ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND        hWnd;
     unsigned    iMessage;
     WORD        wParam;
     LONG        lParam;
     {
     HDC         hDC ;
     PAINTSTRUCT ps;

     switch (iMessage)
          {
          case WM_CREATE :
               SetInternational () ;
               break ;

          case WM_TIMER :
               InvalidateRect (hWnd, NULL, FALSE) ;
               break ;

          case WM_PAINT :
               hDC = BeginPaint (hWnd, &ps) ;
               WndPaint (hWnd, hDC) ;
               EndPaint (hWnd, &ps) ;
               break ;

          case WM_WININICHANGE :
               SetInternational () ;
               InvalidateRect (hWnd, NULL, TRUE) ;
               break ;

          case WM_DESTROY :
               KillTimer (hWnd, 1) ;
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
               break;
          }
     return (0L) ;
     }
