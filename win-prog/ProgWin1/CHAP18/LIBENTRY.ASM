; LIBENTRY.ASM -- Entry point for library modules (small model)
; -------------------------------------------------------------

          Extrn LibMain:Near

_TEXT     SEGMENT BYTE PUBLIC 'CODE'
          ASSUME  CS:_TEXT
          PUBLIC  LibEntry

LibEntry  PROC FAR

          Push DI        ; hInstance
          Push DS        ; Data Segment
          Push CX        ; Heap Size
          Push ES
          Push SI        ; Command Line

          Call LibMain

          Ret

LibEntry  ENDP
_TEXT     ENDS

End       LibEntry
