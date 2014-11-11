/* STRLIB.C -- Library module for STRPROG program */

#include <windows.h>
#include "winundoc.h"

HANDLE hStrings [256] ;
short  nTotal = 0 ;

int PASCAL LibMain (hInstance, wDataSeg, wHeapSize, lpCmdLine)
     HANDLE hInstance ;
     WORD   wDataSeg, wHeapSize ;
     LPSTR  lpCmdLine ;
     {
     if (wHeapSize == 0)
          return 0 ;

     return LocalInit (wDataSeg, NULL, (NPSTR) wHeapSize) ;
     }

BOOL FAR PASCAL AddString (lpStringIn)
     LPSTR  lpStringIn ;
     {
     HANDLE hString ;
     NPSTR  npString ;
     short  i, nLength, nCompare ;

     if (nTotal == 255)
          return FALSE ;

     if (0 == (nLength = lstrlen (lpStringIn)))
          return FALSE ;

     if (NULL == (hString = LocalAlloc (LHND, 1 + nLength)))
          return FALSE ;

     npString = LocalLock (hString) ;
     lstrcpy (npString, lpStringIn) ;
     AnsiUpper (npString) ;
     LocalUnlock (hString) ;

     for (i = nTotal ; i > 0 ; i--)
          {
          npString = LocalLock (hStrings [i - 1]) ;
          nCompare = lstrcmp (lpStringIn, npString) ;
          LocalUnlock (hStrings [i - 1]) ;

          if (nCompare > 0)
               {
               hStrings [i] = hString ;
               break ;
               }
          hStrings [i] = hStrings [i - 1] ;
          }

     if (i == 0)
          hStrings [0] = hString ;

     nTotal++ ;
     return TRUE ;
     }

BOOL FAR PASCAL DeleteString (lpStringIn)
     LPSTR lpStringIn ;
     {
     NPSTR npString ;
     short i, j, nCompare ;

     if (0 == lstrlen (lpStringIn))
          return FALSE ;

     for (i = 0 ; i < nTotal ; i++)
          {
          npString = LocalLock (hStrings [i]) ;
          nCompare = lstrcmp (npString, lpStringIn) ;
          LocalUnlock (hStrings [i]) ;

          if (nCompare == 0)
               break ;
          }

     if (i == nTotal)
          return FALSE ;

     for (j = i ; j < nTotal ; j++)
          hStrings [j] = hStrings [j + 1] ;

     nTotal-- ;
     return TRUE ;
     }     

short FAR PASCAL GetStrings (lpfnGetStrCallBack, lpParam)
     FARPROC lpfnGetStrCallBack ;
     LPSTR   lpParam ;
     {
     BOOL    bReturn ;
     NPSTR   npString ;
     short   i ;

     for (i = 0 ; i < nTotal ; i++)
          {
          npString = LocalLock (hStrings [i]) ;
          bReturn = (*lpfnGetStrCallBack) ((LPSTR) npString, lpParam) ;
          LocalUnlock (hStrings [i]) ;

          if (bReturn == FALSE)
               return i + 1 ;
          }
     return nTotal ;
     }
