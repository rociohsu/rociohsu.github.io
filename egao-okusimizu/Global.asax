<%@ Application Language="C#" %>
<%@ Import Namespace="System.ServiceModel.Activation" %>
<%@ Import Namespace="System.Web.Routing" %>
<script runat="server">

    void Application_BeginRequest(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("X-FRAME-OPTIONS", "SAMEORIGIN");
    }
    void Application_Start(object sender, EventArgs e)
    {
        Application["on_line"] = 0;
        Application["off_line"] = 0;
       // RegisterRoutes(RouteTable.Routes);
        //RegisterRoutes(RouteTable.Routes);
        //// Code that runs on application startup

    }

    private void RegisterRoutes(RouteCollection routes)
    {
       // routes.MapPageRoute("news", "news", "~/news.aspx"); // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
      
       // routes.MapPageRoute("article", "news-detail/{newsid}", "~/news-detail.aspx"); // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    }
    void Application_PreSendRequestHeaders(object sender, EventArgs e)
    {
        HttpResponse response = HttpContext.Current.Response;
        response.Headers.Remove("X-AspNet-Version");  //移除.net 版本
        //response.Headers.Remove("Server");  //移除主機資訊,  X-Powered-By: asp.net 要在 web.config 的<httpprotocal>移除
        //response.Headers.Set("Server") = "";

    }
    //void RegisterRoutes(RouteCollection routes) {
    //    //routes.MapPageRoute("新聞內容","news/{carsty}","news-detail.aspx");
    //    //routes.MapPageRoute("新聞列表","news/","news.aspx");
    //    routes.MapPageRoute("contactus", "contactus", "~/contactus.aspx"); // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("aboutus", "about", "~/about.aspx"); // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("article", "article/{id}", "~/article.aspx"); // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("keyword", "search/{keyword}", "~/search.aspx"); // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("article_category", "article_category/{article_type}", "~/search.aspx"); // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("article_tag", "article_tag/{article_tag}", "~/search.aspx"); // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("logout", "logout/", "~/logout.ashx"); // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("login", "login/", "~/login.aspx"); // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("member_info", "member/info", "~/member_info.aspx");            // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("member_fav", "member/favorite", "~/member_favorite.aspx");            // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("member_pros", "member/pros/{id}", "~/member_pros.aspx");            // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("member_pro", "member/pro", "~/member_pro.aspx");            // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("member_logic", "member/logic", "~/member_logic.aspx");            // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("member_diary", "member/diary", "~/member_diary.aspx");            // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("member", "member/", "~/member.aspx");            // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("price", "price/", "~/price.aspx");            // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("register", "register/", "~/register.aspx");      // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("profile", "pro/{id}", "~/profile.aspx");      // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    routes.MapPageRoute("profile_contact", "profile/contact/{id}", "~/profile_contact.aspx");      // 網址輸入 //localhost/hello/jack 時會導到 某.aspx處理, 如: blog/jack
    //    //routes.MapPageRoute("短網址轉址", "go/{key}", "URL_REDIRECT.ashx");          // 網址輸入 //localhost/go/yahoo 時, 會被導到 _redirect.ashx 純轉址用, 如短網址轉址用時.
    //    //routes.MapPageRoute("語系", "{lang}/{*names}", "url_language.aspx");  // 網址輸入 //localhost/tw/ 時會導到 某_urlmapping.aspx, 並傳回參數 lang=tw 處理.可以應用在多國語言,任何檔案都可對應到絶對路徑
    //    ////以上三種case 可完成三種不同的需求. 
    //}


    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
        Application["on_line"] = 0;
        Application["off_line"] = 0;
    }

    void Application_Error(object sender, EventArgs e)
    {
        //Exception ex = Server.GetLastError();
        //if(ex is HttpException && ((HttpException)ex).GetHttpCode()==404)
        //{
        //    Response.Redirect("error.aspx");
        //}



        //Exception objErr = Server.GetLastError().GetBaseException();
        //if (objErr != null)
        //{
        //    string err = "Error Caught in Application_Error event\n" +
        //            "Error in: " + Request.Url.ToString() +
        //             "\nError Message:" + objErr.Message.ToString() +
        //             "\nStack Trace:" + objErr.StackTrace.ToString();
        //     Session["Page_Error_MSG"] = err;
        // }
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

        Application["on_line"] = (int)Application["on_line"] + 1;
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        //Application["on_line"] = (int)Application["off_line"] + 1;
    }

</script>
