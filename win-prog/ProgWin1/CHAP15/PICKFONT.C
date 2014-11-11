/* PICKFONT.C -- Font Picker Program */

#include <windows.h>
#include "pickfont.h"

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;
BOOL FAR PASCAL DlgProc (HWND, unsigned, WORD, LONG) ;

char    szAppName [] = "PickFont" ;
DWORD   dwAspectMatch = 0L ;
HWND    hDlg ;
LOGFONT lf ;
short   nMapMode = IDD_TEXT ;

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
     hWnd = CreateWindow (szAppName, "Font Picker",
                         WS_OVERLAPPEDWINDOW | WS_CLIPCHILDREN,
                         CW_USEDEFAULT, 0,
                         CW_USEDEFAULT, 0,
                         NULL, NULL, hInstance, NULL) ;

     ShowWindow (hWnd, nCmdShow) ;
     UpdateWindow (hWnd) ;

     while (GetMessage (&msg, NULL, 0, 0))
          {
          if (hDlg == 0 || !IsDialogMessage (hDlg, &msg))
               {
               TranslateMessage (&msg) ;
               DispatchMessage  (&msg) ;
               }
          }
     return msg.wParam ;
     }

void MySetMapMode (hDC)
     HDC  hDC ;
     {
     if (nMapMode == IDD_LTWPS)
          {
          SetMapMode (hDC, MM_ANISOTROPIC) ;
          SetWindowExt (hDC, 1440, 1440) ;
          SetViewportExt (hDC, GetDeviceCaps (hDC, LOGPIXELSX),
                               GetDeviceCaps (hDC, LOGPIXELSY)) ;
          }
     else
          SetMapMode (hDC, MM_TEXT + nMapMode - IDD_TEXT) ;
     }

void ShowMetrics (hDlg)
     HWND hDlg ;
     {
     static TEXTMETRIC tm ;
     static struct 
          {
          short nDlgID ;
          short *pData ;
          }
          shorts [] = 
          {
          TM_HEIGHT,     &tm.tmHeight,
          TM_ASCENT,     &tm.tmAscent,
          TM_DESCENT,    &tm.tmDescent,
          TM_INTLEAD,    &tm.tmInternalLeading,
          TM_EXTLEAD,    &tm.tmExternalLeading,
          TM_AVEWIDTH,   &tm.tmAveCharWidth,
          TM_MAXWIDTH,   &tm.tmMaxCharWidth,
          TM_WEIGHT,     &tm.tmWeight,
          TM_OVER,       &tm.tmOverhang,
          TM_DIGX,       &tm.tmDigitizedAspectX,
          TM_DIGY,       &tm.tmDigitizedAspectY
          } ;
     static char    *szFamily [] = { "Don't Care", "Roman",  "Swiss",
                                     "Modern",     "Script", "Decorative" } ;
     BOOL           bTrans ;
     char           szFaceName [LF_FACESIZE] ;
     HDC            hDC ;
     HFONT          hFont ;
     short          i ;

     lf.lfHeight    = GetDlgItemInt (hDlg, IDD_HEIGHT, &bTrans, TRUE) ;
     lf.lfWidth     = GetDlgItemInt (hDlg, IDD_WIDTH,  &bTrans, FALSE) ;
     lf.lfWeight    = GetDlgItemInt (hDlg, IDD_WEIGHT, &bTrans, FALSE) ;

     lf.lfItalic    = (BYTE) (IsDlgButtonChecked (hDlg, IDD_ITALIC) ? 1 : 0) ;
     lf.lfUnderline = (BYTE) (IsDlgButtonChecked (hDlg, IDD_UNDER)  ? 1 : 0) ;
     lf.lfStrikeOut = (BYTE) (IsDlgButtonChecked (hDlg, IDD_STRIKE) ? 1 : 0) ;

     GetDlgItemText (hDlg, IDD_FACE, lf.lfFaceName, LF_FACESIZE) ;

     dwAspectMatch = IsDlgButtonChecked (hDlg, IDD_ASPECT) ? 1L : 0L ;

     hDC = GetDC (hDlg) ;
     MySetMapMode (hDC) ;
     SetMapperFlags (hDC, dwAspectMatch) ;

     hFont = SelectObject (hDC, CreateFontIndirect (&lf)) ;
     GetTextMetrics (hDC, &tm) ;
     GetTextFace (hDC, sizeof szFaceName, szFaceName) ;

     DeleteObject (SelectObject (hDC, hFont)) ;
     ReleaseDC (hDlg, hDC) ;

     for (i = 0 ; i < sizeof shorts / sizeof shorts [0] ; i++)
          SetDlgItemInt (hDlg, shorts[i].nDlgID, *shorts[i].pData, TRUE) ;

     SetDlgItemText (hDlg, TM_PITCH, tm.tmPitchAndFamily & 1 ?
                                                      "VARIABLE":"FIXED") ;

     SetDlgItemText (hDlg, TM_FAMILY, szFamily [tm.tmPitchAndFamily >> 4]) ;
     SetDlgItemText (hDlg, TM_CHARSET, tm.tmCharSet ? "OEM" : "ANSI") ;
     SetDlgItemText (hDlg, TF_NAME, szFaceName) ;
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
               CheckRadioButton (hDlg, IDD_TEXT,   IDD_LTWPS,  IDD_TEXT) ; 
               CheckRadioButton (hDlg, IDD_ANSI,   IDD_OEM,    IDD_ANSI) ;
               CheckRadioButton (hDlg, IDD_QDRAFT, IDD_QPROOF, IDD_QDRAFT) ;
               CheckRadioButton (hDlg, IDD_PDEF,   IDD_PVAR,   IDD_PDEF) ;
               CheckRadioButton (hDlg, IDD_DONT,   IDD_DEC,    IDD_DONT) ;

               lf.lfEscapement    = 0 ;
               lf.lfOrientation   = 0 ;
               lf.lfOutPrecision  = OUT_DEFAULT_PRECIS ;
               lf.lfClipPrecision = CLIP_DEFAULT_PRECIS ;

               ShowMetrics (hDlg) ;
                                        /* fall through */
          case WM_SETFOCUS:
               SetFocus (GetDlgItem (hDlg, IDD_HEIGHT)) ;
               return FALSE ;

          case WM_COMMAND:
               switch (wParam)
                    {
                    case IDD_TEXT:
                    case IDD_LOMET:
                    case IDD_HIMET:
                    case IDD_LOENG:
                    case IDD_HIENG:
                    case IDD_TWIPS:
                    case IDD_LTWPS:
                         CheckRadioButton (hDlg, IDD_TEXT, IDD_LTWPS, wParam) ;
                         nMapMode = wParam ;
                         break ;

                    case IDD_ASPECT:
                    case IDD_ITALIC:
                    case IDD_UNDER:
                    case IDD_STRIKE:
                         CheckDlgButton (hDlg, wParam, 
                              IsDlgButtonChecked (hDlg, wParam) ? 0 : 1) ;
                         break ;

                    case IDD_ANSI:
                    case IDD_OEM:
                         CheckRadioButton (hDlg, IDD_ANSI, IDD_OEM, wParam) ;
                         lf.lfCharSet = (BYTE) (wParam == IDD_ANSI ? 0 : 255) ;
                         break ;

                    case IDD_QDRAFT:
                    case IDD_QDEF:
                    case IDD_QPROOF:
                         CheckRadioButton (hDlg, IDD_QDRAFT, IDD_QPROOF,
                                                                      wParam) ;
                         lf.lfQuality = (BYTE) (wParam - IDD_QDRAFT) ;
                         break ;

                    case IDD_PDEF:
                    case IDD_PFIXED:
                    case IDD_PVAR:
                         CheckRadioButton (hDlg, IDD_PDEF, IDD_PVAR, wParam) ;
                         lf.lfPitchAndFamily &= 0xF0 ;
                         lf.lfPitchAndFamily |= (BYTE) (wParam - IDD_PDEF) ;
                         break ;

                    case IDD_DONT:
                    case IDD_ROMAN:
                    case IDD_SWISS:
                    case IDD_MODERN:
                    case IDD_SCRIPT:
                    case IDD_DEC:
                         CheckRadioButton (hDlg, IDD_DONT, IDD_DEC, wParam) ;
                         lf.lfPitchAndFamily &= 0x0F ;
                         lf.lfPitchAndFamily |= (BYTE) (wParam-IDD_DONT << 4) ;
                         break ;

                    case IDD_OK:
                         ShowMetrics (hDlg) ;
                         InvalidateRect (GetParent (hDlg), NULL, TRUE) ;
                         break ;
                    }
               break ;

          default:
               return FALSE ;
          }
     return TRUE ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND         hWnd;
     unsigned     iMessage;
     WORD         wParam;
     LONG         lParam;
     {
     static char  szText [] =
                      "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPqQqRrSsTtUuVvWwXxYyZz" ;
     static short xClient, yClient ;
     HANDLE       hInstance ;
     HDC          hDC ;
     HFONT        hFont ;
     FARPROC      lpfnDlgProc ;
     PAINTSTRUCT  ps ;
     RECT         rect ;
     
     switch (iMessage)
          {
          case WM_CREATE :
               hInstance = ((LPCREATESTRUCT) lParam)->hInstance ;
               lpfnDlgProc = MakeProcInstance (DlgProc, hInstance) ;
               hDlg = CreateDialog (hInstance, szAppName, hWnd, lpfnDlgProc) ;
               break ;

          case WM_SETFOCUS:
               SetFocus (hDlg) ;
               break ;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;
               MySetMapMode (hDC) ;
               SetMapperFlags (hDC, dwAspectMatch) ;
               GetClientRect (hDlg, &rect) ;
               rect.bottom += 1 ;
               DPtoLP (hDC, (LPPOINT) &rect, 2) ;

               hFont = SelectObject (hDC, CreateFontIndirect (&lf)) ;

               TextOut (hDC, rect.left, rect.bottom, szText, 52) ;

               DeleteObject (SelectObject (hDC, hFont)) ;
               EndPaint (hWnd, &ps) ;
               break ;

          case WM_DESTROY:
               PostQuitMessage (0) ;
               break ;

          default :
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
