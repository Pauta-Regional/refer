/* JUSTIFY.C -- Justified Type Program */

#include <windows.h>
#include <string.h>
#include <stdio.h>
#include "winundoc.h"
#include "justify.h"

typedef struct {
     short      nNumFaces ;
     char       szFaceNames [MAX_FACES] [LF_FACESIZE] ;
     }
     ENUMFACE ;

typedef struct {
     short      nNumSizes ;
     short      nAspect ;
     LOGFONT    lf [MAX_SIZES] ;
     TEXTMETRIC tm [MAX_SIZES] ;
     }
     ENUMSIZE ;

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;
int  FAR PASCAL EnumAllFaces (LPLOGFONT, LPTEXTMETRIC, short, ENUMFACE FAR *) ;
int  FAR PASCAL EnumAllSizes (LPLOGFONT, LPTEXTMETRIC, short, ENUMSIZE FAR *) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE   hInstance, hPrevInstance ;
     LPSTR    lpszCmdLine ;
     int      nCmdShow ;
     {
     static   char szAppName [] = "Justify" ;
     HWND     hWnd ;
     MSG      msg ;
     WNDCLASS wndclass ;

     if (!hPrevInstance) 
          {
          wndclass.style         = CS_HREDRAW | CS_VREDRAW | CS_OWNDC ;
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
     hWnd = CreateWindow (szAppName, "Justified Type",
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

int FAR PASCAL EnumAllFaces (lplf, lptm, nFontType, lpef)
     LPLOGFONT    lplf ;
     LPTEXTMETRIC lptm ;
     short        nFontType ;
     ENUMFACE FAR *lpef ;
     {
     if (nFontType & RASTER_FONTTYPE)
          {
          lstrcpy (lpef->szFaceNames[lpef->nNumFaces], lplf->lfFaceName) ;
          if (++lpef->nNumFaces == MAX_FACES)
               return 0 ;
          }
     return 1 ;
     }

int FAR PASCAL EnumAllSizes (lplf, lptm, nFontType, lpes)
     LPLOGFONT    lplf ;
     LPTEXTMETRIC lptm ;
     short        nFontType ;
     ENUMSIZE FAR *lpes ;
     {
     if (lpes->nAspect == (100 * lptm->tmDigitizedAspectY /
                                 lptm->tmDigitizedAspectX))
          {
          lpes->lf [lpes->nNumSizes] = *lplf ;
          lpes->tm [lpes->nNumSizes] = *lptm ;
          if (++lpes->nNumSizes == MAX_SIZES)
               return 0 ;
          }
     return 1 ;
     }

short MakeSizeMenu (hWnd, lpfnEnumAllSizes, pes, szFaceName)
     HWND           hWnd ;
     FARPROC        lpfnEnumAllSizes ;
     ENUMSIZE       *pes ;
     char           *szFaceName ;
     {
     static LOGFONT lfBlank ;
     char           szBuffer [20] ;
     HDC            hDC    = GetDC (hWnd) ;
     HMENU          hPopup = GetSubMenu (GetMenu (hWnd), SIZE_MENU) ;
     short          i ;

     pes->nNumSizes = 0 ;
     EnumFonts (hDC, szFaceName, lpfnEnumAllSizes, (LPSTR) pes) ;
     ReleaseDC (hWnd, hDC) ;

     while (GetMenuItemCount (hPopup) > 0)
          ChangeMenu (hPopup, 0, NULL, NULL, MF_DELETE | MF_BYPOSITION) ;

     if (pes->nNumSizes)
          for (i = 0 ; i < pes->nNumSizes ; i++)
               {
               sprintf (szBuffer, "%i  %2d / %2d", i + 1,
                    (pes->tm[i].tmHeight - pes->tm[i].tmInternalLeading + 10)
                                                                        / 20,
                    (pes->tm[i].tmHeight + 10) / 20) ;
               ChangeMenu (hPopup, 0, szBuffer, IDM_ISIZE + i, MF_APPEND) ;
               }
     else           /* no fonts found that match aspect ratio of display */
          {
          pes->lf[0] = lfBlank ;
          strcpy (pes->lf[0].lfFaceName, szFaceName) ;
          ChangeMenu (hPopup, 0, "Default", IDM_ISIZE, MF_APPEND) ;
          }

     CheckMenuItem (hPopup, IDM_ISIZE, MF_CHECKED) ;
     return 0 ;
     }

void DrawRuler (hDC, ptClient)
     HDC          hDC ;
     POINT        ptClient ;
     {
     static short nRuleSize [16] = { 360, 72, 144, 72, 216, 72, 144, 72,
                                     288, 72, 144, 72, 216, 72, 144, 72 } ;
     short        i, j ;

     MoveTo (hDC, 0,          -360) ;
     LineTo (hDC, ptClient.x, -360) ;
     MoveTo (hDC, -360,          0) ;
     LineTo (hDC, -360, ptClient.y) ;

     for (i = 0, j = 0 ; i <= ptClient.x ; i += 1440 / 16, j++)
          {
          MoveTo (hDC, i, -360) ;
          LineTo (hDC, i, -360 - nRuleSize [j % 16]) ;
          }
     for (i = 0, j = 0 ; i <= ptClient.y ; i += 1440 / 16, j++)
          {
          MoveTo (hDC, -360, i) ;
          LineTo (hDC, -360 - nRuleSize [j % 16], i) ;
          }
     }

void Justify (hDC, hResource, ptClient, nCurAlign)
     HDC    hDC ;
     HANDLE hResource ;
     POINT  ptClient ;
     short  nCurAlign ;
     {
     DWORD  dwExtent ;
     LPSTR  lpText, lpBegin, lpEnd ;
     short  i, xStart, yStart, nBreakCount ;

     lpText = LockResource (hResource) ;

     yStart = 0 ;
     do                            /* for each text line */
          {
          nBreakCount = 0 ;
          while (*lpText == ' ')   /* skip over leading blanks */
               lpText++ ;
          lpBegin = lpText ;

          do                       /* until the line is known */
               {
               lpEnd = lpText ;

               while (*lpText != '\0' && *lpText++ != ' ') ;
               if (*lpText == '\0')
                    break ;
                                   /* for each space, calculate extents */
               nBreakCount++ ;
               SetTextJustification (hDC, 0, 0) ;
               dwExtent = GetTextExtent (hDC, lpBegin, lpText - lpBegin - 1) ;
               }
          while (LOWORD (dwExtent) < ptClient.x) ;

          nBreakCount-- ;
          while (*(lpEnd - 1) == ' ')   /* eliminate trailing blanks */
               {
               lpEnd-- ;
               nBreakCount-- ;
               }

          if (*lpText == '\0' || nBreakCount <= 0)
               lpEnd = lpText ;

          SetTextJustification (hDC, 0, 0) ;
          dwExtent = GetTextExtent (hDC, lpBegin, lpEnd - lpBegin) ;

          if (nCurAlign == IDM_LEFT)         /* use alignment for xStart */
               xStart = 0 ;

          else if (nCurAlign == IDM_RIGHT)
               xStart = ptClient.x - LOWORD (dwExtent) ;

          else if (nCurAlign == IDM_CENTER)
               xStart = (ptClient.x - LOWORD (dwExtent)) / 2 ;

          else
               {
               if (*lpText != '\0' && nBreakCount > 0)
                    SetTextJustification (hDC, ptClient.x - LOWORD (dwExtent),
                                                  nBreakCount) ;
               xStart = 0 ;
               }

          TextOut (hDC, xStart, yStart, lpBegin, lpEnd - lpBegin) ;
          yStart += HIWORD (dwExtent) ;
          lpText = lpEnd ;
          }
     while (*lpText && yStart < ptClient.y) ;

     GlobalUnlock (hResource) ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND            hWnd ;
     unsigned        iMessage ;
     WORD            wParam ;
     LONG            lParam ;
     {
     static ENUMFACE ef ;
     static ENUMSIZE es ;
     static FARPROC  lpfnEnumAllFaces, lpfnEnumAllSizes ;
     static HANDLE   hResource ;
     static POINT    ptClient ;
     static short    nCurSize, nCurFace, nCurAttr, nCurAlign = IDM_LEFT ;
     HANDLE          hInstance ;
     HDC             hDC ;
     HFONT           hFont ;
     HMENU           hMenu, hPopup ;
     PAINTSTRUCT     ps ;
     short           xLogPixPerInch, yLogPixPerInch, i ;

     switch (iMessage)
          {
          case WM_CREATE:
               hDC = GetDC (hWnd) ;
               xLogPixPerInch = GetDeviceCaps (hDC, LOGPIXELSX) ;
               yLogPixPerInch = GetDeviceCaps (hDC, LOGPIXELSY) ;

                              /* Set Map Mode */

               SetMapMode (hDC, MM_ANISOTROPIC) ;
               SetWindowExt (hDC, 1440, 1440) ;
               SetViewportExt (hDC, xLogPixPerInch, yLogPixPerInch) ;
               SetWindowOrg (hDC, -720, -720) ;
               es.nAspect = (100 * xLogPixPerInch) / yLogPixPerInch ;

                              /* MakeProcInstance for 2 routines */

               hInstance = ((LPCREATESTRUCT) lParam)-> hInstance ;
               lpfnEnumAllFaces = MakeProcInstance (EnumAllFaces, hInstance) ;
               lpfnEnumAllSizes = MakeProcInstance (EnumAllSizes, hInstance) ;

                              /* Enumerate the Font Faces */

               EnumFonts (hDC, NULL, lpfnEnumAllFaces, (LPSTR) &ef) ;
               ReleaseDC (hWnd, hDC) ;

                              /* Initialize the Menus */

               hMenu  = GetMenu (hWnd) ;
               hPopup = CreateMenu () ;

               for (i = 0 ; i < ef.nNumFaces ; i++)
                    ChangeMenu (hPopup, 0, ef.szFaceNames[i],
                                                  IDM_IFACE + i, MF_APPEND) ;
               ChangeMenu (hMenu, IDM_FACE, "&FaceName", hPopup,
                                                  MF_CHANGE | MF_POPUP) ;
               CheckMenuItem (hMenu, IDM_IFACE, MF_CHECKED) ;

               nCurSize = MakeSizeMenu (hWnd, lpfnEnumAllSizes, &es,
                                   ef.szFaceNames [nCurFace]) ;

                              /* Load the Text Resource */

               hResource = LoadResource (hInstance, 
                           FindResource (hInstance, "Ismael", "TEXT")) ;
               break ;

          case WM_SIZE:
               hDC = GetDC (hWnd) ;
               ptClient = MAKEPOINT (lParam) ;
               DPtoLP (hDC, &ptClient, 1) ;
               ptClient.x -= 360 ;
               ReleaseDC (hWnd, hDC) ;
               break ;

          case WM_COMMAND:
               hMenu = GetMenu (hWnd) ;

               if (wParam >= IDM_IFACE && wParam < IDM_IFACE + MAX_FACES)
                    {
                    CheckMenuItem (hMenu, nCurFace + IDM_IFACE, MF_UNCHECKED) ;
                    CheckMenuItem (hMenu, wParam, MF_CHECKED) ;
                    nCurFace = wParam - IDM_IFACE ;

                    nCurSize = MakeSizeMenu (hWnd, lpfnEnumAllSizes, &es,
                                        ef.szFaceNames [nCurFace]) ;
                    }

               else if (wParam >= IDM_ISIZE && wParam < IDM_ISIZE + MAX_SIZES)
                    {
                    CheckMenuItem (hMenu, nCurSize + IDM_ISIZE, MF_UNCHECKED) ;
                    CheckMenuItem (hMenu, wParam, MF_CHECKED) ;
                    nCurSize = wParam - IDM_ISIZE ;
                    }

               else switch (wParam)
                    {
                    case IDM_BOLD:
                    case IDM_ITALIC:
                    case IDM_STRIKE:
                    case IDM_UNDER:
                         CheckMenuItem (hMenu, wParam, MF_CHECKED &
                              GetMenuState (hMenu, wParam, MF_BYCOMMAND) ?
                                   MF_UNCHECKED : MF_CHECKED) ;
                         nCurAttr ^= wParam ;
                         break ;

                    case IDM_NORM:
                         nCurAttr = 0 ;
                         CheckMenuItem (hMenu, IDM_BOLD,   MF_UNCHECKED) ;
                         CheckMenuItem (hMenu, IDM_ITALIC, MF_UNCHECKED) ;
                         CheckMenuItem (hMenu, IDM_STRIKE, MF_UNCHECKED) ;
                         CheckMenuItem (hMenu, IDM_UNDER,  MF_UNCHECKED) ;
                         break ;

                    case IDM_LEFT:
                    case IDM_RIGHT:
                    case IDM_CENTER:
                    case IDM_JUST:
                         CheckMenuItem (hMenu, nCurAlign, MF_UNCHECKED) ;
                         nCurAlign = wParam ;
                         CheckMenuItem (hMenu, nCurAlign, MF_CHECKED) ;
                         break ;
                    }
               InvalidateRect (hWnd, NULL, TRUE) ;
               break ;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;

               es.lf[nCurSize].lfWeight    = nCurAttr & IDM_BOLD ? 700 : 400 ;
               es.lf[nCurSize].lfItalic    = (BYTE) (nCurAttr & IDM_ITALIC) ;
               es.lf[nCurSize].lfUnderline = (BYTE) (nCurAttr & IDM_UNDER) ;
               es.lf[nCurSize].lfStrikeOut = (BYTE) (nCurAttr & IDM_STRIKE) ;
                                          
               hFont = CreateFontIndirect (&es.lf[nCurSize]) ;
               hFont = SelectObject (hDC, hFont) ;

               DrawRuler (hDC, ptClient) ;
               Justify (hDC, hResource, ptClient, nCurAlign) ;

               DeleteObject (SelectObject (hDC, hFont)) ;
               EndPaint (hWnd, &ps) ;
               break ;

          case WM_DESTROY:
               FreeResource (hResource) ;
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
