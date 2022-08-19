using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.DataBase;
using Shop.Services;

var builder = WebApplication.CreateBuilder(args); //�������������йܷ���������builder

// Add services to the container.ע���������
//ע����ӿ���������MVC���
builder.Services.AddControllers(setaction =>
    {
        setaction.ReturnHttpNotAcceptable = true; //�����ܴ����Http����
    }).AddXmlDataContractSerializerFormatters() //���Xml��ʽ��֧��
    .ConfigureApiBehaviorOptions(setupAction =>
    {
        //�Ƿ�ģ��״̬��Ӧ����
        setupAction.InvalidModelStateResponseFactory = context =>
        {
            var problemDetail = new ValidationProblemDetails(context.ModelState)
            {
                Type = "�����",
                Title = "������֤ʧ��",
                Status = StatusCodes.Status422UnprocessableEntity,
                Detail = "�뿴��ϸ˵��",
                Instance = context.HttpContext.Request.Path //����·��
            };
            problemDetail.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
            return new UnprocessableEntityObjectResult(problemDetail) //���طǷ�ģ��״̬
            {
                ContentTypes = {"application/problem+json"} //������Ӧ��ʽ
            };
        };
    }); //������֤ʧ�ܵ�״̬��422
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); //ע��API����
builder.Services.AddSwaggerGen(); //���SWagger����
//ע�����ݲֿ����������
builder.Services.AddTransient<IGoodsRepository, GoodsRepository>();
//������ݿ�ķ�������ע��
builder.Services.AddDbContext<AppDbContext>(option =>
{
    //����SQL Server���������������ݿ�
    option.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"));
});
//���AutoMapper���Զ�ɨ������Profiles�ļ�
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build(); //���������йܷ�����

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) //����ǿ�������
{
    //����Swagger����
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); //����HTTP�ض������

app.UseAuthorization(); //����·����֤����

app.MapControllers(); //����ӳ�����

app.Run(); //���������йܷ�����