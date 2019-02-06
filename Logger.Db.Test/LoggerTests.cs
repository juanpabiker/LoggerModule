using Logger.Db;
using Logger.Db.Context;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Logger.Db.Test
{
    public class LoggerTests
    {    
        [Fact]
        public void Log_SaveLog()
        {
            var mockSet = new Mock<DbSet<Log>>();
            var mockContext = new Mock<LoggerContext>(string.Empty);
            mockContext.Setup(m => m.Logs).Returns(mockSet.Object);

            // Act
            var repository = new LoggerRepository(mockContext.Object);
            repository.AddLog("Error", "Test");

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Log>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);                      
        }
    }
}
