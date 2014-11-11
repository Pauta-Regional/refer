/*  SCRAMBLE.C -- Scramble (and Unscramble) Screen */

#include <windows.h>
#include <stdlib.h>
#define   NUM  200 

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE       hInstance, hPrevInstance ;
     LPSTR        lpszCmdLine ;
     int          nCmdShow ;
     {
     static short nKeep [NUM][4] ;
     HDC          hDC     = CreateDC ("DISPLAY", NULL, NULL, NULL) ;
     HDC          hMemDC  = CreateCompatibleDC (hDC) ;
     short        xSize   = GetSystemMetrics (SM_CXSCREEN) / 10 ;
     short        ySize   = GetSystemMetrics (SM_CYSCREEN) / 10 ;
     HBITMAP      hBitmap = CreateCompatibleBitmap (hDC, xSize, ySize) ;
     short        i, j, x1, y1, x2, y2 ;

     SelectObject (hMemDC, hBitmap) ;

     srand (LOWORD (GetCurrentTime ())) ;

     for (i = 0 ; i < 2 ; i++)
          for (j = 0 ; j < NUM ; j++)
               {
               if (i == 0)
                    {
                    nKeep [j] [0] = x1 = xSize * (rand () % 10) ;
                    nKeep [j] [1] = y1 = ySize * (rand () % 10) ;
                    nKeep [j] [2] = x2 = xSize * (rand () % 10) ;
                    nKeep [j] [3] = y2 = ySize * (rand () % 10) ;
                    }
               else
                    {
                    x1 = nKeep [NUM - 1 - j] [0] ;
                    y1 = nKeep [NUM - 1 - j] [1] ;
                    x2 = nKeep [NUM - 1 - j] [2] ;
                    y2 = nKeep [NUM - 1 - j] [3] ;
                    }
               BitBlt (hMemDC, 0, 0, xSize, ySize, hDC,  x1, y1, SRCCOPY) ;
               BitBlt (hDC,  x1, y1, xSize, ySize, hDC,  x2, y2, SRCCOPY) ;
               BitBlt (hDC,  x2, y2, xSize, ySize, hMemDC, 0, 0, SRCCOPY) ;
               }
     return FALSE ;
     }
