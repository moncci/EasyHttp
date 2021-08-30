using EasyHttp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EasyHttp.Tool
{
    public class ContentTypeHelper
    {
        private List<ContentTypeItem> _list = null;

        private object _lockObj = new object();
        private static ContentTypeHelper _instance;

        public static ContentTypeHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ContentTypeHelper();
                }
                return _instance;
            }
        }

        private ContentTypeHelper()
        {
            Ini();
        }

        private void Ini()
        {
            _list = new List<ContentTypeItem>();
            _list.Add(new ContentTypeItem() { Name = "	.*	", TypeValue = "	application/octet-stream	 " });
            _list.Add(new ContentTypeItem() { Name = "	0.001	", TypeValue = "	application/x-001	 " });
            _list.Add(new ContentTypeItem() { Name = "	0.323	", TypeValue = "	text/h323	 " });
            _list.Add(new ContentTypeItem() { Name = "	0.907	", TypeValue = "	drawing/907	 " });
            _list.Add(new ContentTypeItem() { Name = "	.acp	", TypeValue = "	audio/x-mei-aac	 " });
            _list.Add(new ContentTypeItem() { Name = "	.aif	", TypeValue = "	audio/aiff	 " });
            _list.Add(new ContentTypeItem() { Name = "	.aiff	", TypeValue = "	audio/aiff	 " });
            _list.Add(new ContentTypeItem() { Name = "	.asa	", TypeValue = "	text/asa	 " });
            _list.Add(new ContentTypeItem() { Name = "	.asp	", TypeValue = "	text/asp	 " });
            _list.Add(new ContentTypeItem() { Name = "	.au	", TypeValue = "	audio/basic	 " });
            _list.Add(new ContentTypeItem() { Name = "	.awf	", TypeValue = "	application/vnd.adobe.workflow	 " });
            _list.Add(new ContentTypeItem() { Name = "	.bmp	", TypeValue = "	application/x-bmp	 " });
            _list.Add(new ContentTypeItem() { Name = "	.c4t	", TypeValue = "	application/x-c4t	 " });
            _list.Add(new ContentTypeItem() { Name = "	.cal	", TypeValue = "	application/x-cals	 " });
            _list.Add(new ContentTypeItem() { Name = "	.cdf	", TypeValue = "	application/x-netcdf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.cel	", TypeValue = "	application/x-cel	 " });
            _list.Add(new ContentTypeItem() { Name = "	.cg4	", TypeValue = "	application/x-g4	 " });
            _list.Add(new ContentTypeItem() { Name = "	.cit	", TypeValue = "	application/x-cit	 " });
            _list.Add(new ContentTypeItem() { Name = "	.cml	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.cmx	", TypeValue = "	application/x-cmx	 " });
            _list.Add(new ContentTypeItem() { Name = "	.crl	", TypeValue = "	application/pkix-crl	 " });
            _list.Add(new ContentTypeItem() { Name = "	.csi	", TypeValue = "	application/x-csi	 " });
            _list.Add(new ContentTypeItem() { Name = "	.cut	", TypeValue = "	application/x-cut	 " });
            _list.Add(new ContentTypeItem() { Name = "	.dbm	", TypeValue = "	application/x-dbm	 " });
            _list.Add(new ContentTypeItem() { Name = "	.dcd	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.der	", TypeValue = "	application/x-x509-ca-cert	 " });
            _list.Add(new ContentTypeItem() { Name = "	.dib	", TypeValue = "	application/x-dib	 " });
            _list.Add(new ContentTypeItem() { Name = "	.doc	", TypeValue = "	application/msword	 " });
            _list.Add(new ContentTypeItem() { Name = "	.drw	", TypeValue = "	application/x-drw	 " });
            _list.Add(new ContentTypeItem() { Name = "	.dwf	", TypeValue = "	Model/vnd.dwf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.dwg	", TypeValue = "	application/x-dwg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.dxf	", TypeValue = "	application/x-dxf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.emf	", TypeValue = "	application/x-emf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ent	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.eps	", TypeValue = "	application/x-ps	 " });
            _list.Add(new ContentTypeItem() { Name = "	.etd	", TypeValue = "	application/x-ebx	 " });
            _list.Add(new ContentTypeItem() { Name = "	.fax	", TypeValue = "	image/fax	 " });
            _list.Add(new ContentTypeItem() { Name = "	.fif	", TypeValue = "	application/fractals	 " });
            _list.Add(new ContentTypeItem() { Name = "	.frm	", TypeValue = "	application/x-frm	 " });
            _list.Add(new ContentTypeItem() { Name = "	.gbr	", TypeValue = "	application/x-gbr	 " });
            _list.Add(new ContentTypeItem() { Name = "	.gif	", TypeValue = "	image/gif	 " });
            _list.Add(new ContentTypeItem() { Name = "	.gp4	", TypeValue = "	application/x-gp4	 " });
            _list.Add(new ContentTypeItem() { Name = "	.hmr	", TypeValue = "	application/x-hmr	 " });
            _list.Add(new ContentTypeItem() { Name = "	.hpl	", TypeValue = "	application/x-hpl	 " });
            _list.Add(new ContentTypeItem() { Name = "	.hrf	", TypeValue = "	application/x-hrf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.htc	", TypeValue = "	text/x-component	 " });
            _list.Add(new ContentTypeItem() { Name = "	.html	", TypeValue = "	text/html	 " });
            _list.Add(new ContentTypeItem() { Name = "	.htx	", TypeValue = "	text/html	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ico	", TypeValue = "	image/x-icon	 " });
            _list.Add(new ContentTypeItem() { Name = "	.iff	", TypeValue = "	application/x-iff	 " });
            _list.Add(new ContentTypeItem() { Name = "	.igs	", TypeValue = "	application/x-igs	 " });
            _list.Add(new ContentTypeItem() { Name = "	.img	", TypeValue = "	application/x-img	 " });
            _list.Add(new ContentTypeItem() { Name = "	.isp	", TypeValue = "	application/x-internet-signup	 " });
            _list.Add(new ContentTypeItem() { Name = "	.java	", TypeValue = "	java/*	 " });
            _list.Add(new ContentTypeItem() { Name = "	.jpe	", TypeValue = "	image/jpeg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.jpeg	", TypeValue = "	image/jpeg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.jpg	", TypeValue = "	application/x-jpg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.jsp	", TypeValue = "	text/html	 " });
            _list.Add(new ContentTypeItem() { Name = "	.lar	", TypeValue = "	application/x-laplayer-reg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.lavs	", TypeValue = "	audio/x-liquid-secure	 " });
            _list.Add(new ContentTypeItem() { Name = "	.lmsff	", TypeValue = "	audio/x-la-lms	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ltr	", TypeValue = "	application/x-ltr	 " });
            _list.Add(new ContentTypeItem() { Name = "	.m2v	", TypeValue = "	video/x-mpeg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.m4e	", TypeValue = "	video/mpeg4	 " });
            _list.Add(new ContentTypeItem() { Name = "	.man	", TypeValue = "	application/x-troff-man	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mdb	", TypeValue = "	application/msaccess	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mfp	", TypeValue = "	application/x-shockwave-flash	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mhtml	", TypeValue = "	message/rfc822	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mid	", TypeValue = "	audio/mid	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mil	", TypeValue = "	application/x-mil	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mnd	", TypeValue = "	audio/x-musicnet-download	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mocha	", TypeValue = "	application/x-javascript	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mp1	", TypeValue = "	audio/mp1	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mp2v	", TypeValue = "	video/mpeg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mp4	", TypeValue = "	video/mpeg4	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mpd	", TypeValue = "	application/vnd.ms-project	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mpeg	", TypeValue = "	video/mpg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mpga	", TypeValue = "	audio/rn-mpeg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mps	", TypeValue = "	video/x-mpeg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mpv	", TypeValue = "	video/mpg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mpw	", TypeValue = "	application/vnd.ms-project	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mtx	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.net	", TypeValue = "	image/pnetvue	 " });
            _list.Add(new ContentTypeItem() { Name = "	.nws	", TypeValue = "	message/rfc822	 " });
            _list.Add(new ContentTypeItem() { Name = "	.out	", TypeValue = "	application/x-out	 " });
            _list.Add(new ContentTypeItem() { Name = "	.p12	", TypeValue = "	application/x-pkcs12	 " });
            _list.Add(new ContentTypeItem() { Name = "	.p7c	", TypeValue = "	application/pkcs7-mime	 " });
            _list.Add(new ContentTypeItem() { Name = "	.p7r	", TypeValue = "	application/x-pkcs7-certreqresp	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pc5	", TypeValue = "	application/x-pc5	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pcl	", TypeValue = "	application/x-pcl	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pdf	", TypeValue = "	application/pdf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pdx	", TypeValue = "	application/vnd.adobe.pdx	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pgl	", TypeValue = "	application/x-pgl	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pko	", TypeValue = "	application/vnd.ms-pki.pko	 " });
            _list.Add(new ContentTypeItem() { Name = "	.plg	", TypeValue = "	text/html	 " });
            _list.Add(new ContentTypeItem() { Name = "	.plt	", TypeValue = "	application/x-plt	 " });
            _list.Add(new ContentTypeItem() { Name = "	.png	", TypeValue = "	application/x-png	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ppa	", TypeValue = "	application/vnd.ms-powerpoint	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pps	", TypeValue = "	application/vnd.ms-powerpoint	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ppt	", TypeValue = "	application/x-ppt	 " });
            _list.Add(new ContentTypeItem() { Name = "	.prf	", TypeValue = "	application/pics-rules	 " });
            _list.Add(new ContentTypeItem() { Name = "	.prt	", TypeValue = "	application/x-prt	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ps	", TypeValue = "	application/postscript	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pwz	", TypeValue = "	application/vnd.ms-powerpoint	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ra	", TypeValue = "	audio/vnd.rn-realaudio	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ras	", TypeValue = "	application/x-ras	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rdf	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.red	", TypeValue = "	application/x-red	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rjs	", TypeValue = "	application/vnd.rn-realsystem-rjs	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rlc	", TypeValue = "	application/x-rlc	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rm	", TypeValue = "	application/vnd.rn-realmedia	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rmi	", TypeValue = "	audio/mid	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rmm	", TypeValue = "	audio/x-pn-realaudio	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rms	", TypeValue = "	application/vnd.rn-realmedia-secure	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rmx	", TypeValue = "	application/vnd.rn-realsystem-rmx	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rp	", TypeValue = "	image/vnd.rn-realpix	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rsml	", TypeValue = "	application/vnd.rn-rsml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rtf	", TypeValue = "	application/msword	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rv	", TypeValue = "	video/vnd.rn-realvideo	 " });
            _list.Add(new ContentTypeItem() { Name = "	.sat	", TypeValue = "	application/x-sat	 " });
            _list.Add(new ContentTypeItem() { Name = "	.sdw	", TypeValue = "	application/x-sdw	 " });
            _list.Add(new ContentTypeItem() { Name = "	.slb	", TypeValue = "	application/x-slb	 " });
            _list.Add(new ContentTypeItem() { Name = "	.slk	", TypeValue = "	drawing/x-slk	 " });
            _list.Add(new ContentTypeItem() { Name = "	.smil	", TypeValue = "	application/smil	 " });
            _list.Add(new ContentTypeItem() { Name = "	.snd	", TypeValue = "	audio/basic	 " });
            _list.Add(new ContentTypeItem() { Name = "	.sor	", TypeValue = "	text/plain	 " });
            _list.Add(new ContentTypeItem() { Name = "	.spl	", TypeValue = "	application/futuresplash	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ssm	", TypeValue = "	application/streamingmedia	 " });
            _list.Add(new ContentTypeItem() { Name = "	.stl	", TypeValue = "	application/vnd.ms-pki.stl	 " });
            _list.Add(new ContentTypeItem() { Name = "	.sty	", TypeValue = "	application/x-sty	 " });
            _list.Add(new ContentTypeItem() { Name = "	.swf	", TypeValue = "	application/x-shockwave-flash	 " });
            _list.Add(new ContentTypeItem() { Name = "	.tg4	", TypeValue = "	application/x-tg4	 " });
            _list.Add(new ContentTypeItem() { Name = "	.tif	", TypeValue = "	image/tiff	 " });
            _list.Add(new ContentTypeItem() { Name = "	.tiff	", TypeValue = "	image/tiff	 " });
            _list.Add(new ContentTypeItem() { Name = "	.top	", TypeValue = "	drawing/x-top	 " });
            _list.Add(new ContentTypeItem() { Name = "	.tsd	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.uin	", TypeValue = "	application/x-icq	 " });
            _list.Add(new ContentTypeItem() { Name = "	.vcf	", TypeValue = "	text/x-vcard	 " });
            _list.Add(new ContentTypeItem() { Name = "	.vdx	", TypeValue = "	application/vnd.visio	 " });
            _list.Add(new ContentTypeItem() { Name = "	.vpg	", TypeValue = "	application/x-vpeg005	 " });
            _list.Add(new ContentTypeItem() { Name = "	.vsd	", TypeValue = "	application/x-vsd	 " });
            _list.Add(new ContentTypeItem() { Name = "	.vst	", TypeValue = "	application/vnd.visio	 " });
            _list.Add(new ContentTypeItem() { Name = "	.vsw	", TypeValue = "	application/vnd.visio	 " });
            _list.Add(new ContentTypeItem() { Name = "	.vtx	", TypeValue = "	application/vnd.visio	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wav	", TypeValue = "	audio/wav	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wb1	", TypeValue = "	application/x-wb1	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wb3	", TypeValue = "	application/x-wb3	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wiz	", TypeValue = "	application/msword	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wk4	", TypeValue = "	application/x-wk4	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wks	", TypeValue = "	application/x-wks	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wma	", TypeValue = "	audio/x-ms-wma	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wmf	", TypeValue = "	application/x-wmf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wmv	", TypeValue = "	video/x-ms-wmv	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wmz	", TypeValue = "	application/x-ms-wmz	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wpd	", TypeValue = "	application/x-wpd	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wpl	", TypeValue = "	application/vnd.ms-wpl	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wr1	", TypeValue = "	application/x-wr1	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wrk	", TypeValue = "	application/x-wrk	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ws2	", TypeValue = "	application/x-ws	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wsdl	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xdp	", TypeValue = "	application/vnd.adobe.xdp	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xfd	", TypeValue = "	application/vnd.adobe.xfd	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xhtml	", TypeValue = "	text/html	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xls	", TypeValue = "	application/x-xls	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xml	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xq	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xquery	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xsl	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xwd	", TypeValue = "	application/x-xwd	 " });
            _list.Add(new ContentTypeItem() { Name = "	.sis	", TypeValue = "	application/vnd.symbian.install	 " });
            _list.Add(new ContentTypeItem() { Name = "	.x_t	", TypeValue = "	application/x-x_t	 " });
            _list.Add(new ContentTypeItem() { Name = "	.apk	", TypeValue = "	application/vnd.android.package-archive	 " });
            _list.Add(new ContentTypeItem() { Name = "	.tif	", TypeValue = "	image/tiff	 " });
            _list.Add(new ContentTypeItem() { Name = "	0.301	", TypeValue = "	application/x-301	 " });
            _list.Add(new ContentTypeItem() { Name = "	0.906	", TypeValue = "	application/x-906	 " });
            _list.Add(new ContentTypeItem() { Name = "	.a11	", TypeValue = "	application/x-a11	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ai	", TypeValue = "	application/postscript	 " });
            _list.Add(new ContentTypeItem() { Name = "	.aifc	", TypeValue = "	audio/aiff	 " });
            _list.Add(new ContentTypeItem() { Name = "	.anv	", TypeValue = "	application/x-anv	 " });
            _list.Add(new ContentTypeItem() { Name = "	.asf	", TypeValue = "	video/x-ms-asf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.asx	", TypeValue = "	video/x-ms-asf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.avi	", TypeValue = "	video/avi	 " });
            _list.Add(new ContentTypeItem() { Name = "	.biz	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.bot	", TypeValue = "	application/x-bot	 " });
            _list.Add(new ContentTypeItem() { Name = "	.c90	", TypeValue = "	application/x-c90	 " });
            _list.Add(new ContentTypeItem() { Name = "	.cat	", TypeValue = "	application/vnd.ms-pki.seccat	 " });
            _list.Add(new ContentTypeItem() { Name = "	.cdr	", TypeValue = "	application/x-cdr	 " });
            _list.Add(new ContentTypeItem() { Name = "	.cer	", TypeValue = "	application/x-x509-ca-cert	 " });
            _list.Add(new ContentTypeItem() { Name = "	.cgm	", TypeValue = "	application/x-cgm	 " });
            _list.Add(new ContentTypeItem() { Name = "	.class	", TypeValue = "	java/*	 " });
            _list.Add(new ContentTypeItem() { Name = "	.cmp	", TypeValue = "	application/x-cmp	 " });
            _list.Add(new ContentTypeItem() { Name = "	.cot	", TypeValue = "	application/x-cot	 " });
            _list.Add(new ContentTypeItem() { Name = "	.crt	", TypeValue = "	application/x-x509-ca-cert	 " });
            _list.Add(new ContentTypeItem() { Name = "	.css	", TypeValue = "	text/css	 " });
            _list.Add(new ContentTypeItem() { Name = "	.dbf	", TypeValue = "	application/x-dbf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.dbx	", TypeValue = "	application/x-dbx	 " });
            _list.Add(new ContentTypeItem() { Name = "	.dcx	", TypeValue = "	application/x-dcx	 " });
            _list.Add(new ContentTypeItem() { Name = "	.dgn	", TypeValue = "	application/x-dgn	 " });
            _list.Add(new ContentTypeItem() { Name = "	.dll	", TypeValue = "	application/x-msdownload	 " });
            _list.Add(new ContentTypeItem() { Name = "	.dot	", TypeValue = "	application/msword	 " });
            _list.Add(new ContentTypeItem() { Name = "	.dtd	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.dwf	", TypeValue = "	application/x-dwf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.dxb	", TypeValue = "	application/x-dxb	 " });
            _list.Add(new ContentTypeItem() { Name = "	.edn	", TypeValue = "	application/vnd.adobe.edn	 " });
            _list.Add(new ContentTypeItem() { Name = "	.eml	", TypeValue = "	message/rfc822	 " });
            _list.Add(new ContentTypeItem() { Name = "	.epi	", TypeValue = "	application/x-epi	 " });
            _list.Add(new ContentTypeItem() { Name = "	.eps	", TypeValue = "	application/postscript	 " });
            _list.Add(new ContentTypeItem() { Name = "	.exe	", TypeValue = "	application/x-msdownload	 " });
            _list.Add(new ContentTypeItem() { Name = "	.fdf	", TypeValue = "	application/vnd.fdf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.fo	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.g4	", TypeValue = "	application/x-g4	 " });
            _list.Add(new ContentTypeItem() { Name = "	.	", TypeValue = "	application/x-	 " });
            _list.Add(new ContentTypeItem() { Name = "	.gl2	", TypeValue = "	application/x-gl2	 " });
            _list.Add(new ContentTypeItem() { Name = "	.hgl	", TypeValue = "	application/x-hgl	 " });
            _list.Add(new ContentTypeItem() { Name = "	.hpg	", TypeValue = "	application/x-hpgl	 " });
            _list.Add(new ContentTypeItem() { Name = "	.hqx	", TypeValue = "	application/mac-binhex40	 " });
            _list.Add(new ContentTypeItem() { Name = "	.hta	", TypeValue = "	application/hta	 " });
            _list.Add(new ContentTypeItem() { Name = "	.htm	", TypeValue = "	text/html	 " });
            _list.Add(new ContentTypeItem() { Name = "	.htt	", TypeValue = "	text/webviewhtml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.icb	", TypeValue = "	application/x-icb	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ico	", TypeValue = "	application/x-ico	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ig4	", TypeValue = "	application/x-g4	 " });
            _list.Add(new ContentTypeItem() { Name = "	.iii	", TypeValue = "	application/x-iphone	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ins	", TypeValue = "	application/x-internet-signup	 " });
            _list.Add(new ContentTypeItem() { Name = "	.IVF	", TypeValue = "	video/x-ivf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.jfif	", TypeValue = "	image/jpeg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.jpe	", TypeValue = "	application/x-jpe	 " });
            _list.Add(new ContentTypeItem() { Name = "	.jpg	", TypeValue = "	image/jpeg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.js	", TypeValue = "	application/x-javascript	 " });
            _list.Add(new ContentTypeItem() { Name = "	.la1	", TypeValue = "	audio/x-liquid-file	 " });
            _list.Add(new ContentTypeItem() { Name = "	.latex	", TypeValue = "	application/x-latex	 " });
            _list.Add(new ContentTypeItem() { Name = "	.lbm	", TypeValue = "	application/x-lbm	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ls	", TypeValue = "	application/x-javascript	 " });
            _list.Add(new ContentTypeItem() { Name = "	.m1v	", TypeValue = "	video/x-mpeg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.m3u	", TypeValue = "	audio/mpegurl	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mac	", TypeValue = "	application/x-mac	 " });
            _list.Add(new ContentTypeItem() { Name = "	.math	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mdb	", TypeValue = "	application/x-mdb	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mht	", TypeValue = "	message/rfc822	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mi	", TypeValue = "	application/x-mi	 " });
            _list.Add(new ContentTypeItem() { Name = "	.midi	", TypeValue = "	audio/mid	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mml	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mns	", TypeValue = "	audio/x-musicnet-stream	 " });
            _list.Add(new ContentTypeItem() { Name = "	.movie	", TypeValue = "	video/x-sgi-movie	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mp2	", TypeValue = "	audio/mp2	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mp3	", TypeValue = "	audio/mp3	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mpa	", TypeValue = "	video/x-mpg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mpe	", TypeValue = "	video/x-mpeg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mpg	", TypeValue = "	video/mpg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mpp	", TypeValue = "	application/vnd.ms-project	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mpt	", TypeValue = "	application/vnd.ms-project	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mpv2	", TypeValue = "	video/mpeg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mpx	", TypeValue = "	application/vnd.ms-project	 " });
            _list.Add(new ContentTypeItem() { Name = "	.mxp	", TypeValue = "	application/x-mmxp	 " });
            _list.Add(new ContentTypeItem() { Name = "	.nrf	", TypeValue = "	application/x-nrf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.odc	", TypeValue = "	text/x-ms-odc	 " });
            _list.Add(new ContentTypeItem() { Name = "	.p10	", TypeValue = "	application/pkcs10	 " });
            _list.Add(new ContentTypeItem() { Name = "	.p7b	", TypeValue = "	application/x-pkcs7-certificates	 " });
            _list.Add(new ContentTypeItem() { Name = "	.p7m	", TypeValue = "	application/pkcs7-mime	 " });
            _list.Add(new ContentTypeItem() { Name = "	.p7s	", TypeValue = "	application/pkcs7-signature	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pci	", TypeValue = "	application/x-pci	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pcx	", TypeValue = "	application/x-pcx	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pdf	", TypeValue = "	application/pdf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pfx	", TypeValue = "	application/x-pkcs12	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pic	", TypeValue = "	application/x-pic	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pl	", TypeValue = "	application/x-perl	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pls	", TypeValue = "	audio/scpls	 " });
            _list.Add(new ContentTypeItem() { Name = "	.png	", TypeValue = "	image/png	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pot	", TypeValue = "	application/vnd.ms-powerpoint	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ppm	", TypeValue = "	application/x-ppm	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ppt	", TypeValue = "	application/vnd.ms-powerpoint	 " });
            _list.Add(new ContentTypeItem() { Name = "	.pr	", TypeValue = "	application/x-pr	 " });
            _list.Add(new ContentTypeItem() { Name = "	.prn	", TypeValue = "	application/x-prn	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ps	", TypeValue = "	application/x-ps	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ptn	", TypeValue = "	application/x-ptn	 " });
            _list.Add(new ContentTypeItem() { Name = "	.r3t	", TypeValue = "	text/vnd.rn-realtext3d	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ram	", TypeValue = "	audio/x-pn-realaudio	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rat	", TypeValue = "	application/rat-file	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rec	", TypeValue = "	application/vnd.rn-recording	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rgb	", TypeValue = "	application/x-rgb	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rjt	", TypeValue = "	application/vnd.rn-realsystem-rjt	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rle	", TypeValue = "	application/x-rle	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rmf	", TypeValue = "	application/vnd.adobe.rmf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rmj	", TypeValue = "	application/vnd.rn-realsystem-rmj	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rmp	", TypeValue = "	application/vnd.rn-rn_music_package	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rmvb	", TypeValue = "	application/vnd.rn-realmedia-vbr	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rnx	", TypeValue = "	application/vnd.rn-realplayer	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rpm	", TypeValue = "	audio/x-pn-realaudio-plugin	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rt	", TypeValue = "	text/vnd.rn-realtext	 " });
            _list.Add(new ContentTypeItem() { Name = "	.rtf	", TypeValue = "	application/x-rtf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.sam	", TypeValue = "	application/x-sam	 " });
            _list.Add(new ContentTypeItem() { Name = "	.sdp	", TypeValue = "	application/sdp	 " });
            _list.Add(new ContentTypeItem() { Name = "	.sit	", TypeValue = "	application/x-stuffit	 " });
            _list.Add(new ContentTypeItem() { Name = "	.sld	", TypeValue = "	application/x-sld	 " });
            _list.Add(new ContentTypeItem() { Name = "	.smi	", TypeValue = "	application/smil	 " });
            _list.Add(new ContentTypeItem() { Name = "	.smk	", TypeValue = "	application/x-smk	 " });
            _list.Add(new ContentTypeItem() { Name = "	.sol	", TypeValue = "	text/plain	 " });
            _list.Add(new ContentTypeItem() { Name = "	.spc	", TypeValue = "	application/x-pkcs7-certificates	 " });
            _list.Add(new ContentTypeItem() { Name = "	.spp	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.sst	", TypeValue = "	application/vnd.ms-pki.certstore	 " });
            _list.Add(new ContentTypeItem() { Name = "	.stm	", TypeValue = "	text/html	 " });
            _list.Add(new ContentTypeItem() { Name = "	.svg	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.tdf	", TypeValue = "	application/x-tdf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.tga	", TypeValue = "	application/x-tga	 " });
            _list.Add(new ContentTypeItem() { Name = "	.tif	", TypeValue = "	application/x-tif	 " });
            _list.Add(new ContentTypeItem() { Name = "	.tld	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.torrent	", TypeValue = "	application/x-bittorrent	 " });
            _list.Add(new ContentTypeItem() { Name = "	.txt	", TypeValue = "	text/plain	 " });
            _list.Add(new ContentTypeItem() { Name = "	.uls	", TypeValue = "	text/iuls	 " });
            _list.Add(new ContentTypeItem() { Name = "	.vda	", TypeValue = "	application/x-vda	 " });
            _list.Add(new ContentTypeItem() { Name = "	.vml	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.vsd	", TypeValue = "	application/vnd.visio	 " });
            _list.Add(new ContentTypeItem() { Name = "	.vss	", TypeValue = "	application/vnd.visio	 " });
            _list.Add(new ContentTypeItem() { Name = "	.vst	", TypeValue = "	application/x-vst	 " });
            _list.Add(new ContentTypeItem() { Name = "	.vsx	", TypeValue = "	application/vnd.visio	 " });
            _list.Add(new ContentTypeItem() { Name = "	.vxml	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wax	", TypeValue = "	audio/x-ms-wax	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wb2	", TypeValue = "	application/x-wb2	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wbmp	", TypeValue = "	image/vnd.wap.wbmp	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wk3	", TypeValue = "	application/x-wk3	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wkq	", TypeValue = "	application/x-wkq	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wm	", TypeValue = "	video/x-ms-wm	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wmd	", TypeValue = "	application/x-ms-wmd	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wml	", TypeValue = "	text/vnd.wap.wml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wmx	", TypeValue = "	video/x-ms-wmx	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wp6	", TypeValue = "	application/x-wp6	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wpg	", TypeValue = "	application/x-wpg	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wq1	", TypeValue = "	application/x-wq1	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wri	", TypeValue = "	application/x-wri	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ws	", TypeValue = "	application/x-ws	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wsc	", TypeValue = "	text/scriptlet	 " });
            _list.Add(new ContentTypeItem() { Name = "	.wvx	", TypeValue = "	video/x-ms-wvx	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xdr	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xfdf	", TypeValue = "	application/vnd.adobe.xfdf	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xls	", TypeValue = "	application/vnd.ms-excel	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xlw	", TypeValue = "	application/x-xlw	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xpl	", TypeValue = "	audio/scpls	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xql	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xsd	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xslt	", TypeValue = "	text/xml	 " });
            _list.Add(new ContentTypeItem() { Name = "	.x_b	", TypeValue = "	application/x-x_b	 " });
            _list.Add(new ContentTypeItem() { Name = "	.sisx	", TypeValue = "	application/vnd.symbian.install	 " });
            _list.Add(new ContentTypeItem() { Name = "	.ipa	", TypeValue = "	application/vnd.iphone	 " });
            _list.Add(new ContentTypeItem() { Name = "	.xap	", TypeValue = "	application/x-silverlight-app	 " });

            foreach (ContentTypeItem item in _list)
            {
                item.Name = item.Name.Trim().ToLower();
                item.TypeValue = item.TypeValue.Trim().ToLower();
            }

        }

        /// <summary>
        /// 获取contentType
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        internal string GetContentTypeByNameIn(string fileName)
        {
            lock (_lockObj)
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    string tempFileName = fileName.Trim().ToLower();
                    string suffix = Path.GetExtension(tempFileName);
                    if (string.IsNullOrEmpty(suffix))
                    {
                        suffix = ".*";
                    }
                    ContentTypeItem item = _list.Where(t => t.Name == suffix).FirstOrDefault();
                    if (item != null)
                    {
                        return item.TypeValue;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 获取contentType
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public string GetContentTypeByName(string fileName)
        {
            return GetContentTypeByNameIn(fileName);
        }
    }
}
