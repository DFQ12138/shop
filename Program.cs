using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.DataBase;
using Shop.Services;

var builder = WebApplication.CreateBuilder(args); //创建管理虚拟托管服务器工具builder

// Add services to the container.注册服务依赖
//注入添加控制器服务MVC框架
builder.Services.AddControllers(setaction =>
    {
        setaction.ReturnHttpNotAcceptable = true; //不接受错误的Http请求
    }).AddXmlDataContractSerializerFormatters() //添加Xml格式的支持
    .ConfigureApiBehaviorOptions(setupAction =>
    {
        //非法模型状态响应工厂
        setupAction.InvalidModelStateResponseFactory = context =>
        {
            var problemDetail = new ValidationProblemDetails(context.ModelState)
            {
                Type = "随便填",
                Title = "数据验证失败",
                Status = StatusCodes.Status422UnprocessableEntity,
                Detail = "请看详细说明",
                Instance = context.HttpContext.Request.Path //请求路径
            };
            problemDetail.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
            return new UnprocessableEntityObjectResult(problemDetail) //返回非法模型状态
            {
                ContentTypes = {"application/problem+json"} //设置响应格式
            };
        };
    }); //数据验证失败的状态码422
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); //注入API服务
builder.Services.AddSwaggerGen(); //添加SWagger服务
//注册数据仓库的依赖服务
builder.Services.AddTransient<IGoodsRepository, GoodsRepository>();
//完成数据库的服务依赖注入
builder.Services.AddDbContext<AppDbContext>(option =>
{
    //调用SQL Server的配置来加载数据库
    option.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"));
});
//添加AutoMapper，自动扫描所有Profiles文件
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build(); //创建虚拟托管服务器

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //如果是开发环境
{
    //调用Swagger服务
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); //调用HTTP重定向服务

app.UseAuthorization(); //调用路由认证服务

app.MapControllers(); //调用映射服务

app.Run(); //运行虚拟托管服务器