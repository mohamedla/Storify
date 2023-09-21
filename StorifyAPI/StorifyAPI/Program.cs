// Copyright 2023 Mohamed Ashraf Tolba
using StorifyAPI;

var builder = WebApplication.CreateBuilder(args);

Startup startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
#region Add service while There is no startup
// Add services to the container.


//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
////builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();

////builder.Services.AddDbContext<StorifyContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Storify") ?? throw new InvalidOperationException("Can't found Storify Connection String While Working With Identity.")));

//builder.Services.AddDefaultIdentity<StoreUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<IdentityContext>();

#region Service Extentions

//builder.Services.ConfigCORS(); // CORS Config Policy
//builder.Services.ConfigIISIntegration(); // IIS Option

#endregion

#region Identity Service
//builder.Services.AddDbContext<IdentityContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Storify") ?? throw new InvalidOperationException("Can't found Storify Connection String While Working With Identity.")));

//builder.Services.AddIdentity<StoreUser, IdentityRole>(
//    option =>
//    {
//        option.Password.RequireDigit = true;
//        option.Password.RequireUppercase = true;
//        option.Password.RequireLowercase = true;
//        option.Password.RequireNonAlphanumeric = true;
//        option.Password.RequiredLength = 8;
//        option.User.RequireUniqueEmail = true;
//    }
//    )
//    .AddEntityFrameworkStores<IdentityContext>()
//    .AddDefaultTokenProviders();
#endregion
#region JWT Config
//var jwtSetting = builder.Configuration.GetSection("JwtSettings");
//var secretKey = builder.Configuration.GetSection("Secret").Value; // better be system environment variable
//builder.Services.AddAuthentication(option =>
//{
//    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(option =>
//{
//    option.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = jwtSetting.GetSection("validIssuer").Value,
//        ValidAudience = jwtSetting.GetSection("validAudience").Value,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
//    };
//});

//builder.Services.AddScoped<IAuthenticationManager, AuthenticationManager>();
#endregion

//builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); 
#endregion


var app = builder.Build();
startup.Configure(app, builder.Environment);

#region app config while There is no startup
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//else //MoAsh
//{
//    app.UseHsts(); // Strict-Transport-Security header
//}

//app.UseHttpsRedirection(); // Enforce Https requests

//#region App Config
//app.UseStaticFiles();
//app.UseCors(ServiceExtensions.corsPolicy);
//app.UseForwardedHeaders(new ForwardedHeadersOptions
//{
//    ForwardedHeaders = ForwardedHeaders.All
//});
//#endregion
////app.UseRouting();
//app.MapControllers();

////Authentification and Authorization middleware
//app.UseAuthentication();
//app.UseAuthorization();



//app.Run(); 
#endregion
