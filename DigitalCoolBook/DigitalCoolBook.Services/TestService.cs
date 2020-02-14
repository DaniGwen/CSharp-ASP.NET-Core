namespace DigitalCoolBook.Service
{
    using DigitalCoolBook.App.Data;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class TestService : ITestService
    {
        private readonly ApplicationDbContext context;

        public TestService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddTestAsync(Test test)
        {
            await this.context.Tests.AddAsync(test);
            await this.SaveChangesAsync();
        }

        public Test GetTest(string id)
        {
            return this.context.Tests.FirstOrDefault(t => t.LessonId == id);
        }

        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
