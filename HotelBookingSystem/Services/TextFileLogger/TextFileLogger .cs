using System;
using System.IO;
using System.Threading.Tasks;

namespace HotelBookingSystem.Services.TextFileLogger
{

    public class TextFileLogger : ITextFileLogger
    {
        private readonly string _defaultLogFilePath;

        public TextFileLogger(string defaultLogFilePath = null)
        {
            if (!string.IsNullOrEmpty(defaultLogFilePath))
            {
                _defaultLogFilePath = defaultLogFilePath;
                EnsureDirectoryExists(_defaultLogFilePath);
            }
            else
            {
                var logDir = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
                if (!Directory.Exists(logDir))
                    Directory.CreateDirectory(logDir);
                _defaultLogFilePath = Path.Combine(logDir, "log.txt");
            }
        }

        public async Task LogAsync(string content, string fullFilePath = null)
        {
            string filePath = fullFilePath ?? _defaultLogFilePath;

            try
            {
                EnsureDirectoryExists(filePath);

                string logContent = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {content}{Environment.NewLine}";

                await File.AppendAllTextAsync(filePath, logContent);
            }
            catch (UnauthorizedAccessException ex)
            {
                // 權限不足，建議記錄到其他地方或通知管理員
                // 這裡示範簡單忽略錯誤，避免影響主流程
                // 可替換為寫入 Windows 事件日誌或其他備援方案
            }
            catch (Exception ex)
            {
                // 其他例外也可視需求處理
            }
        }

        private void EnsureDirectoryExists(string filePath)
        {
            var dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}
