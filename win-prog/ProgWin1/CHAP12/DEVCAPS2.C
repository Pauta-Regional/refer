/* DEVCAPS2.C -- Display routines for DEVCAPS */

#include <windows.h>
#include <string.h>
#include <stdio.h>

typedef struct
     {
     short nMask ;
     char  *szMask ;
     char  *szDesc ;
     }
     BITS ;

void DoBasicInfo (hDC, hIC, xChar, yChar)
     HDC    hDC, hIC ;
     short  xChar, yChar ;
     {
     static struct
          {
          short nIndex ;
          char  *szDesc ;
          }
          info [] =
          {
          HORZSIZE,      "HORZSIZE     Width in millimeters:",
          VERTSIZE,      "VERTSIZE     Height in millimeters:",
          HORZRES,       "HORZRES      Width in pixels:",
          VERTRES,       "VERTRES      Height in raster lines:",
          BITSPIXEL,     "BITSPIXEL    Color bits per pixel:",
          PLANES,        "PLANES       Number of color planes:",
          NUMBRUSHES,    "NUMBRUSHES   Number of device brushes:",
          NUMPENS,       "NUMPENS      Number of device pens:",
          NUMMARKERS,    "NUMMARKERS   Number of device markers:",
          NUMFONTS,      "NUMFONTS     Number of device fonts:",
          NUMCOLORS,     "NUMCOLORS    Number of device colors:",
          PDEVICESIZE,   "PDEVICESIZE  Size of device structure:",
          ASPECTX,       "ASPECTX      Relative width of pixel:",
          ASPECTY,       "ASPECTY      Relative height of pixel:",
          ASPECTXY,      "ASPECTXY     Relative diagonal of pixel:",
          LOGPIXELSX,    "LOGPIXELSX   Horizontal dots per inch:",
          LOGPIXELSY,    "LOGPIXELSY   Vertical dots per inch:"
          } ;
     char   szBuffer [80] ;
     short  i, nLine ;

     for (i = 0 ; i < sizeof info / sizeof info [0] ; i++)
          TextOut (hDC, xChar, (i + 1) * yChar, szBuffer,
               sprintf (szBuffer, "%-40s%8d", info[i].szDesc,
                    GetDeviceCaps (hIC, info[i].nIndex))) ;
     }

void DoOtherInfo (hDC, hIC, xChar, yChar)
     HDC         hDC, hIC ;
     short       xChar, yChar ;
     {
     static BITS clip [] =
          {
          CP_RECTANGLE,  "CP_RECTANGLE",     "Can Clip To Rectangle:"
          } ; 

     static BITS raster [] =
          {
          RC_BITBLT,     "RC_BITBLT",        "Capable of simple BitBlt:",
          RC_BANDING,    "RC_BANDING",       "Requires banding support:",
          RC_SCALING,    "RC_SCALING",       "Requires scaling support:",
          RC_BITMAP64,   "RC_BITMAP64",      "Supports bitmaps >64K:"
          } ;

     static char *szTech [] = { "DT_PLOTTER (Vector plotter)",
                                "DT_RASDISPLAY (Raster display)",
                                "DT_RASPRINTER (Raster printer)",
                                "DT_RASCAMERA (Raster camera)",
                                "DT_CHARSTREAM (Character-stream, PLP)",
                                "DT_METAFILE (Metafile, VDM)",
                                "DT_DISPFILE (Display-file)" } ;
     char        szBuffer [80] ;
     short       i ;

     TextOut (hDC, xChar, yChar, szBuffer,
          sprintf (szBuffer, "%-24s%04XH",
               "DRIVERVERSION:", GetDeviceCaps (hIC, DRIVERVERSION))) ;

     TextOut (hDC, xChar, 2 * yChar, szBuffer,
          sprintf (szBuffer, "%-24s%-40s",
               "TECHNOLOGY:", szTech [GetDeviceCaps (hIC, TECHNOLOGY)])) ;

     TextOut (hDC, xChar, 4 * yChar, szBuffer,
          sprintf (szBuffer, "CLIPCAPS (Clipping capabilities)")) ;

     for (i = 0 ; i < sizeof clip / sizeof clip [0] ; i++)
          TextOut (hDC, 9 * xChar, (i + 6) * yChar, szBuffer,
               sprintf (szBuffer, "%-16s%-28s %3s",
                    clip[i].szMask, clip[i].szDesc,
                    GetDeviceCaps (hIC, CLIPCAPS) & clip[i].nMask ?
                         "Yes" : "No")) ;

     TextOut (hDC, xChar, 8 * yChar, szBuffer,
          sprintf (szBuffer, "RASTERCAPS (Raster capabilities)")) ;

     for (i = 0 ; i < sizeof raster / sizeof raster [0] ; i++)
          TextOut (hDC, 9 * xChar, (i + 10) * yChar, szBuffer,
               sprintf (szBuffer, "%-16s%-28s %3s",
                    raster[i].szMask, raster[i].szDesc,
                    GetDeviceCaps (hIC, RASTERCAPS) & raster[i].nMask ?
                         "Yes" : "No")) ;
     }

void DoBitCodedCaps (hDC, hIC, xChar, yChar, nType)
     HDC         hDC, hIC ;
     short       xChar, yChar, nType ;
     {
     static BITS curves [] =
          {
          CC_CIRCLES,    "CC_CIRCLES",    "circles:",
          CC_PIE,        "CC_PIE",        "pie wedges:",
          CC_CHORD,      "CC_CHORD",      "chord arcs:",
          CC_ELLIPSES,   "CC_ELLIPSES",   "ellipses:",
          CC_WIDE,       "CC_WIDE",       "wide borders:",
          CC_STYLED,     "CC_STYLED",     "styled borders:",
          CC_WIDESTYLED, "CC_WIDESTYLED", "wide and styled borders:",
          CC_INTERIORS,  "CC_INTERIORS",  "interiors:"
          } ; 

     static BITS lines [] =
          {
          LC_POLYLINE,   "LC_POLYLINE",   "polyline:",
          LC_MARKER,     "LC_MARKER",     "markers:",
          LC_POLYMARKER, "LC_POLYMARKER", "polymarkers",
          LC_WIDE,       "LC_WIDE",       "wide lines:",
          LC_STYLED,     "LC_STYLED",     "styled lines:",
          LC_WIDESTYLED, "LC_WIDESTYLED", "wide and styled lines:",
          LC_INTERIORS,  "LC_INTERIORS",  "interiors:"
          } ;

     static BITS poly [] =
          {
          PC_POLYGON,    "PC_POLYGON",    "alternate fill polygon:",
          PC_RECTANGLE,  "PC_RECTANGLE",  "rectangle:",
          PC_TRAPEZOID,  "PC_TRAPEZOID",  "winding number fill polygon:",
          PC_SCANLINE,   "PC_SCANLINE",   "scanlines:",
          PC_WIDE,       "PC_WIDE",       "wide borders:",
          PC_STYLED,     "PC_STYLED",     "styled borders:",
          PC_WIDESTYLED, "PC_WIDESTYLED", "wide and styled borders:",
          PC_INTERIORS,  "PC_INTERIORS",  "interiors:"
          } ;

     static BITS text [] =
          {
          TC_OP_CHARACTER, "TC_OP_CHARACTER", "character output precision:",
          TC_OP_STROKE,    "TC_OP_STROKE",    "stroke output precision:",
          TC_CP_STROKE,    "TC_CP_STROKE",    "stroke clip precision:",
          TC_CR_90,        "TC_CP_90",        "90 degree character rotation:",
          TC_CR_ANY,       "TC_CR_ANY",       "any character rotation:",
          TC_SF_X_YINDEP,  "TC_SF_X_YINDEP", "scaling independent of X and Y:",
          TC_SA_DOUBLE,    "TC_SA_DOUBLE",   "doubled character for scaling:",
          TC_SA_INTEGER,   "TC_SA_INTEGER",  "integer multiples for scaling:",
          TC_SA_CONTIN,    "TC_SA_CONTIN",  "any multiples for exact scaling:",
          TC_EA_DOUBLE,    "TC_EA_DOUBLE",  "double weight characters:",
          TC_IA_ABLE,      "TC_IA_ABLE",    "italicizing:",
          TC_UA_ABLE,      "TC_UA_ABLE",    "underlining:",
          TC_SO_ABLE,      "TC_SO_ABLE",    "strikeouts:",
          TC_RA_ABLE,      "TC_RA_ABLE",    "raster fonts:",
          TC_VA_ABLE,      "TC_VA_ABLE",    "vector fonts:"
          } ;

     static struct
          {
          short nIndex ;
          char  *szTitle ;
          BITS  (*pbits) [] ;
          short nSize ;
          }
          bitinfo [] =
          {
          CURVECAPS,  "CURVCAPS (Curve Capabilities)",
                      (BITS (*)[]) curves, sizeof curves / sizeof curves [0],
          LINECAPS,   "LINECAPS (Line Capabilities)",
                      (BITS (*)[]) lines, sizeof lines / sizeof lines [0],
          POLYGONALCAPS, "POLYGONALCAPS (Polygonal Capabilities)",
                      (BITS (*)[]) poly, sizeof poly / sizeof poly [0],
          TEXTCAPS,   "TEXTCAPS (Text Capabilities)",
                      (BITS (*)[]) text, sizeof text / sizeof text [0]
          } ;
     static char szBuffer [80] ;
     BITS        (*pbits) [] = bitinfo [nType].pbits ;
     short       nDevCaps = GetDeviceCaps (hIC, bitinfo [nType].nIndex) ;
     short       i ;

     TextOut (hDC, xChar, yChar, bitinfo [nType].szTitle,
                    strlen (bitinfo [nType].szTitle)) ;

     for (i = 0 ; i < bitinfo [nType].nSize ; i++)
          TextOut (hDC, xChar, (i + 3) * yChar, szBuffer,
               sprintf (szBuffer, "%-16s %s %-32s %3s",
                    (*pbits)[i].szMask, "Can do", (*pbits)[i].szDesc,
                    nDevCaps & (*pbits)[i].nMask ? "Yes" : "No")) ;
     }
