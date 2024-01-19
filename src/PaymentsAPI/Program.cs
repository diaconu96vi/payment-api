using PaymentsAPI.Services.Payments;
using PaymentsAPI.Services.Transactions;
using PaymentsAPI.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddTransient<IConcurrencyChecker, ConcurrencyChecker>();
builder.Services.AddSingleton<PaymentTracker>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
