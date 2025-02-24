<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="news-detail.aspx.cs" Inherits="_Default" %>
<html lang="zh-Hant">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=1.0,user-scalable=0,shrink-to-fit=no">
<meta http-equiv="content-language" content="zh-Hant-TW">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
<title><%=mtpc + mtpcs + "|" %>高橋酒造店-源自1885年</title>
<meta name="keywords" content="吟釀,大吟釀,梅酒,日本清酒,日本梅酒,美鄉雪華,職人釀造">
<meta name="description" content="<%=mmems %>">
<meta property="og:title" content="<%=mtpc+mtpcs %>" >
<meta property="og:type" content="website" >
<meta property="og:description" content="<%=mmems %>" >
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
			<a class="logo" href="index.html"><img src="/images/logo.png"></a>
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
			<h1 class="title"><b>最新消息</b></h1>
			<div class="news-detail">
				<div class="pic" data-aos="fade-up" data-aos-duration="600"><img src="<%=mimg %>"></div>
				<div class="right" data-aos="fade-up" data-aos-duration="600">
					<div class="title"><%=mtpc %><br><%=mtpcs %></div>
					<div class="date"><%=msdt %></div>
					<div class="content">
						<%=mmem %>
						 <%   Response.Write( aa.cms_show("新聞","",int.Parse(msno),1,"../",ref mlnks,ref mpics,ref mmsg) );   %>
					</div>
				</div>
				<div class="tag" data-aos="zoom-in" data-aos-duration="600"><%=mtypnam %></div>
			</div><!-- END .news-detail -->
			<div class="arrowbtn">
				<a class="btn prev hvr-fade" href="news.aspx"><i></i> 消息一覽</a>
				<%if (psno != "0") { %><a class="btn prev hvr-fade" href="news-detail.aspx?newsid=<%=psno %>"><i></i> 上一篇消息</a><%} %>
				<%if (nsno != "0") { %><a class="btn next hvr-fade" href="news-detail.aspx?newsid=<%=nsno %>">下一篇消息 <i></i></a><%} %>
			</div>
		</div>
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