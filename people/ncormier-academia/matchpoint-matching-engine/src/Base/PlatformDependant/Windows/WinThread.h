/******************************************************************************
 ******************************************************************************
 * Copyright (c) 2007 MatchPoint, All rights reserved
 *
 * File    :    WinThread.h
 * Desc    :    Thread declaration for windows
 * Author  :    nico
 ******************************************************************************/

#ifndef __WIN_THREAD_H__
# define __WIN_THREAD_H__

/*****************************************************************************/
/* Header files                                                              */
/*****************************************************************************/
# include <windows.h>
# include "Thread.h"
/*****************************************************************************/
/* Default namespace                                                         */
/*****************************************************************************/
# include "DefaultNamespace.h"
DNSPACE_OPEN
/*****************************************************************************/
/* Types Definition                                                          */
/*****************************************************************************/
class WinThread : public Thread
{
  /***************************************************************************/
  /* Methods                                                                 */
  /***************************************************************************/
public:
                WinThread(fThreadStartRoutine entryPoint, void* pvData);
  virtual      ~WinThread(void);
  void          Start(void);
  void          Suspend(void);
  void          Resume(void);
  void          Terminate(void);
  /***************************************************************************/
  /* Properties                                                              */
  /***************************************************************************/
private:
  HANDLE    m_thread;
};
/*****************************************************************************/
/* Default namespace close                                                   */
/*****************************************************************************/
DNSPACE_CLOSE

#endif /* __THREAD_H__ */
