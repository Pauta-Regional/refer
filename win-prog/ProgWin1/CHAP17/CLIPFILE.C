/* CLIPFILE.C -- Clipboard Load and Save */

#include <windows.h>
#include <string.h>
#include <stdio.h>
#include "winundoc.h"
#include "clipfile.h"
#include "filedlg.h"

extern BOOL DoFileOpenDlg (HANDLE, HWND, PSTR, PSTR, WORD,   PSTR, POFSTRUCT) ;
extern BOOL DoFileSaveDlg (HANDLE, HWND, PSTR, PSTR, WORD *, PSTR, POFSTRUCT) ;

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

typedef struct
     {
     WORD  dummy1 ;
     short bmType ;
     short bmWidth ;
     short bmHeight ;
     short bmWidthBytes ;
     BYTE  bmPlanes ;
     BYTE  bmBitsPixel ;
     DWORD dummy2 ;
     }
     BMFILEHDR ;

char szAppName []    = "ClipFile" ;
char *szFileSpecs [] = { "*.TXT", "*.BMP", "*.WMF", "*.SYL", "*.DIF" } ;

FARPROC        lpfnMFPInfo ;
HANDLE         hInst ;
LPMETAFILEPICT lpmfp ;

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
          wndclass.hIcon         = LoadIcon (hInstance, szAppName) ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
          wndclass.hbrBackground = COLOR_WINDOW + 1 ;
          wndclass.lpszMenuName  = szAppName ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }

     hInst = hInstance ;

     hWnd = CreateWindow (szAppName, NULL,
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

BOOL FAR PASCAL MFPInfoDlgProc (hDlg, iMessage, wParam, lParam)
     HWND     hDlg ;
     unsigned iMessage ;
     WORD     wParam ;
     LONG     lParam ;
     {
     BOOL     bTrans ;

     switch (iMessage)
          {
          case WM_INITDIALOG:
               CheckRadioButton (hDlg, IDD_TEXT, IDD_ANISO, IDD_TEXT) ;
               SetDlgItemInt (hDlg, IDD_XEXT, 0, FALSE) ;
               SetDlgItemInt (hDlg, IDD_YEXT, 0, FALSE) ;
               lpmfp->mm = MM_TEXT ;

               SetFocus (GetDlgItem (hDlg, IDD_TEXT)) ;
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
                    case IDD_ISOTR:
                    case IDD_ANISO:
                         CheckRadioButton (hDlg, IDD_TEXT, IDD_ANISO, wParam) ;
                         lpmfp->mm = MM_TEXT + wParam - IDD_TEXT ;
                         break ;

                    case IDOK:
                         lpmfp->xExt = GetDlgItemInt (hDlg, IDD_XEXT,
                                                       &bTrans, TRUE) ;
                         lpmfp->yExt = GetDlgItemInt (hDlg, IDD_YEXT,
                                                       &bTrans, TRUE) ;
                         EndDialog (hDlg, TRUE) ;
                         break ;
               
                    case IDCANCEL:
                         EndDialog (hDlg, FALSE) ;
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

void OkMessageBox (hWnd, szMessage, szFileName)
     char *szMessage, szFileName ;
     {
     char szBuffer [80] ;
     
     sprintf (szBuffer, szMessage, szFileName) ;
     MessageBox (hWnd, szBuffer, szAppName, MB_OK | MB_ICONEXCLAMATION) ;
     }

void PrepareMetaFile (hDC, lpmfp, xClient, yClient)
     HDC            hDC ;
     LPMETAFILEPICT lpmfp ;
     short          xClient, yClient ;
     {
     long           xlScale, ylScale, lScale ;

     SetMapMode (hDC, lpmfp->mm) ;

     if (lpmfp->mm == MM_ISOTROPIC || lpmfp->mm == MM_ANISOTROPIC)
          {
          if (lpmfp->xExt == 0)

               SetViewportExt (hDC, xClient, yClient) ;

          else if (lpmfp->xExt > 0)

               SetViewportExt (hDC,
                    (short) ((long) lpmfp->xExt *
                              GetDeviceCaps (hDC, HORZRES) /
                              GetDeviceCaps (hDC, HORZSIZE) / 100),
                    (short) ((long) lpmfp->yExt *
                              GetDeviceCaps (hDC, VERTRES) /
                              GetDeviceCaps (hDC, VERTSIZE) / 100)) ;

          else if (lpmfp->xExt < 0)
               {
               xlScale = 100L * (long) xClient *
                                        GetDeviceCaps (hDC, HORZSIZE) /
                                        GetDeviceCaps (hDC, HORZRES) /
                                             -lpmfp->xExt ;
               ylScale = 100L * (long) yClient *
                                        GetDeviceCaps (hDC, VERTSIZE) /
                                        GetDeviceCaps (hDC, VERTRES) /
                                             -lpmfp->yExt ;
               lScale = min (xlScale, ylScale) ;

               SetViewportExt (hDC,
                    (short) ((long) -lpmfp->xExt * lScale *
                              GetDeviceCaps (hDC, HORZRES) /
                              GetDeviceCaps (hDC, HORZSIZE) / 100),
                    (short) ((long) -lpmfp->yExt * lScale *
                              GetDeviceCaps (hDC, VERTRES) /
                              GetDeviceCaps (hDC, VERTSIZE) / 100)) ;
               }
          }
     }

long _lfilelength (hFile)
     HANDLE hFile ;
     {
     long   lCurrentPos = _llseek (hFile, 0L, 1) ;
     long   lFileLength = _llseek (hFile, 0L, 2) ;
    
     _llseek (hFile, lCurrentPos, 0) ;

     return lFileLength ;
     }

void LoadFile (hWnd, wFormat)
     HWND      hWnd ;
     WORD      wFormat ;
     {
     BMFILEHDR bmhdr ;
     char      szFileName [16] ;
     HANDLE    hFile, hGMem, hMF ;
     HBITMAP   hBitmap ;
     LPSTR     lpGMem ;
     OFSTRUCT  of ;
     WORD      wLength ;

     if (!DoFileOpenDlg (hInst, hWnd, szFileSpecs [wFormat - IDM_TXT],
               szFileSpecs [wFormat - IDM_TXT] + 1, 0x4010, szFileName, &of))
          return ;

     if (-1 == (hFile = OpenFile (szFileName, &of, OF_READ | OF_REOPEN)))
          {
          OkMessageBox (hWnd, "Cannot open file %s", szFileName) ;
          return ;
          }

     if (_lfilelength (hFile) > 65535)
          {
          _lclose (hFile) ;
          OkMessageBox (hWnd, "File %s too large", szFileName) ;
          return ;
          }

     wLength = (WORD) _lfilelength (hFile) ;

     if (NULL == (hGMem = GlobalAlloc (GHND, (DWORD) wLength + 1)))
          {
          _lclose (hFile) ;
          OkMessageBox (hWnd, "Cannot allocate memory for %s", szFileName) ;
          return ;
          }

     switch (wFormat)
          {
          case IDM_TXT:
          case IDM_SYL:
          case IDM_DIF:
               lpGMem = GlobalLock (hGMem) ;
               _lread (hFile, lpGMem, wLength) ;
               _lclose (hFile) ;

               lpGMem [wLength] = '\0' ;
               GlobalUnlock (hGMem) ;
               break ;

          case IDM_BMP:
               _lread (hFile, (LPSTR) &bmhdr, sizeof (BMFILEHDR)) ;

               wLength = bmhdr.bmPlanes * bmhdr.bmWidthBytes * bmhdr.bmHeight ;

               lpGMem = GlobalLock (hGMem) ;
               _lread (hFile, lpGMem, wLength) ;
               _lclose (hFile) ;

               hBitmap = CreateBitmap (bmhdr.bmWidth, bmhdr.bmHeight,
                              bmhdr.bmPlanes, bmhdr.bmBitsPixel, lpGMem) ;

               GlobalUnlock (hGMem) ;
               GlobalFree (hGMem) ;

               if (NULL == (hGMem = hBitmap))
                    {
                    OkMessageBox (hWnd, "Could not create bitmap", NULL) ;
                    return ;
                    }
               break ;

          case IDM_WMF:
               lpGMem = GlobalLock (hGMem) ;
               _lread (hFile, lpGMem, wLength) ;
               _lclose (hFile) ;
               GlobalUnlock (hGMem) ;
               hMF = SetMetaFileBits (hGMem) ;

               hGMem = GlobalAlloc (GHND, (DWORD) sizeof (METAFILEPICT)) ;
               lpmfp = (LPMETAFILEPICT) GlobalLock (hGMem) ;
               lpmfp->hMF = hMF ;

               if (!DialogBox (hInst, "MFPInfo", hWnd, lpfnMFPInfo))
                    {
                    GlobalUnlock (hGMem) ;
                    GlobalFree (hGMem) ;
                    DeleteMetaFile (hMF) ;
                    return ;
                    }

               GlobalUnlock (hGMem) ;
               break ;
          }

     OpenClipboard (hWnd) ;
     EmptyClipboard () ;
     SetClipboardData (wFormat, hGMem) ;
     CloseClipboard () ;
     return ;
     }

void SaveFile (hWnd, wFormat)
     HWND      hWnd ;
     WORD      wFormat ;
     {
     BMFILEHDR bmhdr ;
     char      szBuffer [80], szFileName [16] ;
     DWORD     dwLength ;
     HANDLE    hFile, hGMem, hMF ;
     HBITMAP   hBitmap ;
     LPSTR     lpGMem ;
     OFSTRUCT  of ;
     WORD      wLength, wWriteBytes, wStatus ;

     if (!DoFileSaveDlg (hInst, hWnd, szFileSpecs [wFormat - IDM_TXT],
               szFileSpecs [wFormat - IDM_TXT] + 1, &wStatus, szFileName, &of))
          return ;

     if (wStatus == 1)
          {
          sprintf (szBuffer, "Replace existing %s", szFileName) ;
          if (IDNO == MessageBox (hWnd, szBuffer, szAppName,
                                        MB_YESNO | MB_ICONQUESTION))
               return ;
          }

     OpenClipboard (hWnd) ;
 
     if (NULL == (hGMem = GetClipboardData (wFormat)))
          {
          CloseClipboard () ;
          OkMessageBox (hWnd, "Clipboard data has disappeared", NULL) ;
          return ;
          }

     switch (wFormat)
          {
          case IDM_TXT:
          case IDM_SYL:
          case IDM_DIF:

               if (GlobalSize (hGMem) > 65535L)
                    {
                    CloseClipboard () ;
                    OkMessageBox (hWnd, "Data too large for file", NULL) ;
                    return ;
                    }

               lpGMem = GlobalLock (hGMem) ;
               wLength = min (lstrlen (lpGMem), (WORD) GlobalSize (hGMem)) ;
               break ;

          case IDM_BMP:
               hBitmap = hGMem ;
               GetObject (hBitmap, sizeof (BITMAP), (LPSTR) &bmhdr.bmType) ;
               bmhdr.dummy1 = 0 ;
               bmhdr.dummy2 = 0 ;

               if ((dwLength = (DWORD) bmhdr.bmPlanes * bmhdr.bmWidthBytes *
                               (DWORD) bmhdr.bmHeight + sizeof (BMFILEHDR))
                                             > 65535L)
                    {
                    CloseClipboard () ;
                    OkMessageBox (hWnd, "Bitmap too large for file", NULL) ;
                    return ;
                    }
               wLength = (WORD) dwLength ;

               if (NULL == (hGMem = GlobalAlloc (GHND, dwLength)))
                    {
                    CloseClipboard () ;
                    OkMessageBox (hWnd, "Cannot allocate memory", NULL) ;
                    return ;
                    }
               lpGMem = GlobalLock (hGMem) ;

               * (BMFILEHDR FAR *) lpGMem = bmhdr ;
               GetBitmapBits (hBitmap, dwLength - sizeof (BMFILEHDR),
                                         lpGMem + sizeof (BMFILEHDR)) ;
               break ;

          case IDM_WMF:

               lpmfp = (LPMETAFILEPICT) GlobalLock (hGMem) ;
               hMF = lpmfp->hMF ;
               GlobalUnlock (hGMem) ;

               hGMem = GetMetaFileBits (hMF) ;

               if (GlobalSize (hGMem) > 65535L)
                    {
                    SetMetaFileBits (hGMem) ;
                    CloseClipboard () ;
                    OkMessageBox (hWnd, "Metafile too large for file", NULL) ;
                    return ;
                    }
                                   
               wLength = (WORD) GlobalSize (hGMem) ;
               lpGMem = GlobalLock (hGMem) ;
               break ;
          }

     if (-1 == (hFile = OpenFile (szFileName, &of, OF_CREATE | OF_REOPEN)))
          OkMessageBox (hWnd, "Cannot create file %s", szFileName) ;
     else
          {
          wWriteBytes = _lwrite (hFile, lpGMem, wLength) ;
          _lclose (hFile) ;
          if (wWriteBytes < wLength)
               {
               OpenFile (szFileName, &of, OF_DELETE | OF_REOPEN) ;
               OkMessageBox (hWnd, "Not enough disk space for %s", szFileName);
               }
          }
     GlobalUnlock (hGMem) ;

     if (wFormat == IDM_BMP)
          GlobalFree (hGMem) ;

     else if (wFormat == IDM_WMF)
          SetMetaFileBits (hGMem) ;

     CloseClipboard () ;
     }

void PaintWindow (hWnd, hDC, wFormat)
     HWND           hWnd ;
     HDC            hDC ;
     WORD           wFormat ;
     {
     static char    *szFormats [] =  { "Text", "Bitmap", "Metafile",
                                       "Sylk", "DIF" },
                    *szMapModes [] = { "MM_TEXT",      "MM_LOMETRIC",
                                       "MM_HIMETRIC",  "MM_LOENGLISH",
                                       "MM_HIENGLISH", "MM_TWIPS",
                                       "MM_ISOTROPIC", "MM_ANISOTROPIC" } ;
     BITMAP         bm ;
     char           szBuffer [80] ;
     HANDLE         hGMem, hMF ;
     HBITMAP        hBitmap ;
     HDC            hMemDC ;
     LPMETAFILEPICT lpmfp ;
     LPSTR          lpGMem ;
     RECT           rect ;
     WORD           wCaption, wLength ;

     GetClientRect (hWnd, &rect) ;
     OpenClipboard (hWnd) ;

     wCaption = sprintf (szBuffer, "ClipFile: %s",
                         szFormats [wFormat - IDM_TXT]) ;

     if (hGMem = GetClipboardData (wFormat))
          {
          switch (wFormat)
               {
               case IDM_TXT:
               case IDM_SYL:
               case IDM_DIF:
                    lpGMem = GlobalLock (hGMem) ;
                    wLength = min (lstrlen (lpGMem),
                                   (WORD) GlobalSize (hGMem)) ;

                    DrawText (hDC, lpGMem, wLength, &rect, DT_EXPANDTABS) ;

                    sprintf (szBuffer + wCaption, ", %u byte%s",
                             wLength, wLength == 1 ? "" : "s") ; 

                    GlobalUnlock (hGMem) ;
                    break ;

               case IDM_BMP:
                    hBitmap = hGMem ;

                    hMemDC = CreateCompatibleDC (hDC) ;
                    SelectObject (hMemDC, hBitmap) ;
                    GetObject (hBitmap, sizeof (BITMAP), (LPSTR) &bm) ;
                    BitBlt (hDC, 0, 0, bm.bmWidth, bm.bmHeight,
                            hMemDC, 0, 0, SRCCOPY) ;
                    DeleteDC (hMemDC) ;

                    sprintf (szBuffer + wCaption,
                             ", %d wide, %d high, %d %s%s, %d bit%s%s",
                             bm.bmWidth, bm.bmHeight, bm.bmPlanes,
                             "plane", bm.bmPlanes == 1 ? "" : "s",
                             bm.bmBitsPixel, bm.bmBitsPixel == 1 ?
                             "" : "s", " per pixel");
                    break ;

               case IDM_WMF:

                    hGMem = GetClipboardData (CF_METAFILEPICT) ;
                    lpmfp = (LPMETAFILEPICT) GlobalLock (hGMem) ;

                    SaveDC (hDC) ;
                    PrepareMetaFile (hDC, lpmfp, rect.right, rect.bottom) ;
                    PlayMetaFile (hDC, lpmfp->hMF) ;
                    RestoreDC (hDC, -1) ;

                    sprintf (szBuffer + wCaption, ", %s, %d by %d",
                             szMapModes [lpmfp->mm - MM_TEXT], 
                             lpmfp->xExt, lpmfp->yExt) ;

                    GlobalUnlock (hGMem) ;
                    break ;
               }
          }
          else
               sprintf (szBuffer + wCaption, " (None)") ;

     CloseClipboard () ;
     SetWindowText (hWnd, szBuffer) ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND        hWnd ;
     unsigned    iMessage ;
     WORD        wParam ;
     LONG        lParam ;
     {
     static HWND hWndNextViewer ;
     static WORD wFormat = IDM_TXT ; /* = CF_TEXT */ 
     HDC         hDC ;
     HMENU       hMenu ;
     PAINTSTRUCT ps ;

     switch (iMessage)
          {
          case WM_CREATE:
               lpfnMFPInfo = MakeProcInstance (MFPInfoDlgProc, hInst) ;
               hWndNextViewer = SetClipboardViewer (hWnd) ;
               break ;

          case WM_CHANGECBCHAIN :
               if (wParam == hWndNextViewer)
                    hWndNextViewer = LOWORD (lParam) ;

               else if (hWndNextViewer)
                    SendMessage (hWndNextViewer, iMessage, wParam, lParam) ;
               break ;

          case WM_DRAWCLIPBOARD :
               if (hWndNextViewer)
                    SendMessage (hWndNextViewer, iMessage, wParam, lParam) ;
               InvalidateRect (hWnd, NULL, TRUE) ;
               break;

          case WM_INITMENUPOPUP:
               if (lParam == 0)
                    EnableMenuItem (wParam, IDM_SAVE, 
                         IsClipboardFormatAvailable (wFormat) ?
                              MF_ENABLED : MF_GRAYED) ;
               break ;

          case WM_COMMAND:

               switch (wParam)
                    {
                    case IDM_TXT:
                    case IDM_BMP:
                    case IDM_WMF:
                    case IDM_SYL:
                    case IDM_DIF:
                         hMenu = GetMenu (hWnd) ;

                         CheckMenuItem (hMenu, wFormat, MF_UNCHECKED) ;
                         wFormat = wParam ;
                         CheckMenuItem (hMenu, wFormat, MF_CHECKED) ;

                         InvalidateRect (hWnd, NULL, TRUE) ;
                         break ;

                    case IDM_LOAD:
                         LoadFile (hWnd, wFormat) ;
                         break ;

                    case IDM_SAVE:
                         SaveFile (hWnd, wFormat) ;
                         break ;
                    }
               break ;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;
               PaintWindow (hWnd, hDC, wFormat) ;
               EndPaint (hWnd, &ps) ;
               break ;

          case WM_DESTROY:
               ChangeClipboardChain (hWnd, hWndNextViewer) ;
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
