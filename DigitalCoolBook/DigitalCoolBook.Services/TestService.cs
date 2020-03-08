﻿namespace DigitalCoolBook.Service
{
    using DigitalCoolBook.App.Data;
    using DigitalCoolBook.Models;
    using DigitalCoolBook.Services.Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TestService : ITestService
    {
        private readonly ApplicationDbContext context;

        public TestService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddExpiredTestAsync(ExpiredTest expiredTest)
        {
            await this.context.ExpiredTests.AddAsync(expiredTest);
            await this.SaveChangesAsync();
        }

        public async Task AddTestAsync(Test test)
        {
            await this.context.Tests.AddAsync(test);
            await this.SaveChangesAsync();
        }

        public async Task AddTestStudentsAsync(List<TestStudent> testStudentList)
        {
            await this.context.AddRangeAsync(testStudentList);
            await this.SaveChangesAsync();
        }

        public async Task<Test> GetTestAsync(string id)
        {
            return await this.context.Tests.FindAsync(id);
        }

        public Test GetTestByLesson(string id)
        {
            return this.context.Tests.FirstOrDefault(t => t.LessonId == id);
        }

        public IQueryable<Test> GetTests()
        {
            return this.context.Tests;
        }

        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
