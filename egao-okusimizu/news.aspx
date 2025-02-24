<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="news.aspx.cs" Inherits="_Default" %>
<html lang="zh-Hant">

<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=0,shrink-to-fit=no">
<meta http-equiv="content-language" content="zh-Hant-TW">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
<title>高橋酒造店-源自1885年</title>
<meta name="keywords" content="吟釀,大吟釀,梅酒,日本清酒,日本梅酒,美鄉雪華,職人釀造">
<meta name="description" content="高橋酒造成立於1885年，由古內杜氏帶領釀製，出產的大吟釀香氣十足奔放，品飲後味道甘甜而滑順。 以百年入魂，傳達極致的職人精神。 一輩子只做這一件事，將靈魂融入酒中。">
<meta property="og:title" content="高橋酒造店-源自1885年" >
<meta property="og:type" content="website" >
<meta property="og:description" content="高橋酒造成立於1885年，由古內杜氏帶領釀製，出產的大吟釀香氣十足奔放，品飲後味道甘甜而滑順。 以百年入魂，傳達極致的職人精神。 一輩子只做這一件事，將靈魂融入酒中。" >
<meta property="og:url" content="http://www.egao-okusimizu.com.tw/"/>
<meta property="og:image" content="http://www.egao-okusimizu.com.tw/images/share_cover.jpg" >
<link rel="icon" type="image/x-icon" href="images/favicon.ico">
<link rel="stylesheet" href="css/layout.css">
<!-- Global site tag (gtag.js) - Google Analytics -->
<script async src="https://www.googletagmanager.com/gtag/js?id=UA-144551697-3"></script>
<script>
  window.dataLayer = window.dataLayer || [];
  function gtag(){dataLayer.push(arguments);}
  gtag('js', new Date());

  gtag('config', 'UA-144551697-3');
</script>
</head>

<body>
	<nav>
		<div class="menu">
			<a class="logo" href="index.html"><img src="images/logo.png"></a>
			<a href="about.html">關於高橋</a>
			<a href="quality.html">製酒の關鍵</a>
			<a href="spirit.html">釀造製程</a>
			<a href="product.html">お酒一覽</a>
			<a class="current" href="news.aspx">最新消息</a>
			<a href="store.html">販售通路</a>
			<a href="contact.html">聯絡我們</a>
		</div>
		<a class="navIcon"><span></span><span></span><span></span><span></span></a>
	</nav>

	<div class="top-bar"><a class="logo" href="index.html"><img src="images/logo-m.png"></a></div>

	<section>
		<div class="banner-top" style="background-image: url(images/news/news-top.jpg);"><b>最新消息</b></div>
		<div class="container">
			<form name="form2" method="post"><input type="hidden" name="page" value="<%=mpage %>" /><input type="hidden" name="b1" />
			<h1 class="title"><b>最新消息</b></h1>
			<%
                int mindex=0;
                int mpc=1;
                int gi = 1;
                string show_page = "";
            int ss, ee;

            aa.show_page1_param(ref dv1, msqlcom, msqlcom2, ref mpage, mc, ref mpc, ref mindex, ref mmsg);

            if (mpage <= 5)
            {
                ss = 1;
                ee = 5;
            }
            else
            {
                ss = mpage - 2;
                ee = mpage + 2;
            }
            if (ee > mpc) ee = mpc;
				
				

                if (dv1 != null)
                {
                    for (int jj = mindex; jj <= dv1.Count - 1; jj++)
                    {
					%>
			<div class="news-list">
				<a class="hvr-fade" href="/news-detail.aspx?newsid=<%=dv1[jj]["sno"] %>" data-aos="fade-up" data-aos-duration="600">
					<span class="pic"><i><img src="<%=aa.chkdbnull(dv1[jj]["img"]) %>"></i></span>
					<span class="right">
						<span class="title"><%=aa.chkdbnull(dv1[jj]["tpc"]) %><br><%=aa.chkdbnull(dv1[jj]["tpcs"]) %></span>
						<span class="date"><%=aa.showdate(aa.chkdbnull(dv1[jj]["sdt"]),"yyyy.MM.dd") %></span>
						<span class="more"><i>more</i></span>
					</span>
					<span class="tag"><%=aa.chkdbnull(dv1[jj]["typnam"]) %></span>
				</a>
			</div>
			<%	}
                    dv1.Dispose();
                } %>
					<div class="wrap-pager text-center news-list">
				<%if (mpage > 1)
											 { %><span class="pager-prev"><a href="javascript:;" onclick="form2.b1.value='q';form2.page.value='<%=mpage - 1 %>';form2.submit();"><i></i> 上一頁</a></span><%} %>

								<ul class="list-pager">
									<!-- ALWAYS SHOW ONLY 5 PAGERS -->
									<%  //Response.Write(ss + "," + ee);
										for (int mpagei = ss; mpagei <= ee; mpagei++)
										{
											if (mpagei == mpage)
											{
                        
												Response.Write("<li class=\"item-pager active\"><a href=\"javascript:;\" onclick=\"form2.action='';form2.b1.value='q';form2.page.value='" + mpagei + "';form2.submit();\" >" + mpagei + "</a></li>");
											}
											else
											{
                      
												Response.Write("<li class=\"item-pager\"><a href=\"javascript:;\" onclick=\"form2.action='';form2.b1.value='q';form2.page.value='" + mpagei + "';form2.submit();\" >" + mpagei + "</a></li>");
											}
										}
                
													 %>
			
								</ul>
				<%if (mpage < mpc)
											 { %><span class="pager-next"><a href="javascript:;" onclick="form2.b1.value='q';form2.page.value='<%=mpage + 1 %>';form2.submit();">下一頁 <i></i></a></span><%} %>
			</div>
		</form>

		<!--<div class="wrap-pager text-center news-list">
			<span class="pager-prev"><a><i></i> 上一頁</a></span>
			<ul class="list-pager">
				<li class="item-pager active"><a>1</a></li>
				<li class="item-pager"><a>2</a></li>
				<li class="item-pager"><a>3</a></li>
				<li class="item-pager"><a>4</a></li>
				<li class="item-pager"><a>5</a></li>
			</ul>
			<span class="pager-next"><a>下一頁 <i></i></a></span>
		</div>-->

		</div><!-- END .container -->
	</section>
	
	<footer>
		<div class="container">
			<img src="images/demo/footer.png">
		</div>
	</footer>

	<div class="drink-slogan">
		<picture>
			<source srcset="images/drink-slogan-m.jpg" media="(max-width: 768px)">
			<source srcset="images/drink-slogan.jpg">
			<img src="images/drink-slogan.jpg" alt="飲酒過量有害健康、禁止酒駕、未滿十八歲禁止飲酒">
		</picture>
	</div>

<!-- JQ -->
<script src="js/jquery-3.4.1.min.js"></script>
<script src="js/jquery-migrate-3.1.0.min.js"></script>
<script src="js/aos.js"></script>
<script src="js/layout.js"></script>
</body>
</html>
