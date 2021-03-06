#include	"unpxti.h"

int
main(int argc, char **argv)
{
	int					tfd, n, flags;
	char				recvline[MAXLINE + 1];
	struct sockaddr_in	servaddr;
	struct t_call		tcall;
	struct t_discon		tdiscon;

	if (argc != 2)
		err_quit("usage: daytimecli01 <IPaddress>");

	tfd = T_open(XTI_TCP, O_RDWR, NULL);

	T_bind(tfd, NULL, NULL);

	bzero(&servaddr, sizeof(servaddr));
	servaddr.sin_family = AF_INET;
	servaddr.sin_port   = htons(13);	/* daytime server */
	Inet_pton(AF_INET, argv[1], &servaddr.sin_addr);

	tcall.addr.maxlen = sizeof(servaddr);
	tcall.addr.len = sizeof(servaddr);
	tcall.addr.buf = &servaddr;

	tcall.opt.len = 0;		/* no options with connect */
	tcall.udata.len = 0;	/* no user data with connect */

	if (t_connect(tfd, &tcall, NULL) < 0) {
		if (t_errno == TLOOK) {
			if ( (n = T_look(tfd)) == T_DISCONNECT) {
				tdiscon.udata.maxlen = 0;
				T_rcvdis(tfd, &tdiscon);
				errno = tdiscon.reason;
				err_sys("t_connect error");
			} else
				err_quit("unexpected event after t_connect: %d", n);
		} else
			err_xti("t_connect error");
	}

	Xti_rdwr(tfd);
	for ( ; ; ) {
		if ( (n = read(tfd, recvline, MAXLINE)) <= 0) {
			if (n == 0)
				break;		/* server closed connection */
			else
				err_sys("read error");
		}
		recvline[n] = 0;	/* null terminate */
		fputs(recvline, stdout);
	}
	exit(0);
}
