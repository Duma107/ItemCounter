using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ItemCounterAPI
{
    // Models
    public class CountRequest
    {
        [Required]
        public List<string> Items { get; set; } = new List<string>();
        
        [Required]
        public string DataType { get; set; } = string.Empty;
    }

    public class CountResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Dictionary<string, int> Counts { get; set; } = new Dictionary<string, int>();
        public string DataType { get; set; } = string.Empty;
        public int TotalItems { get; set; }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    // Services
    public interface IItemCounterService
    {
        CountResponse CountItems(CountRequest request);
        List<string> GetSupportedDataTypes();
    }

    public class ItemCounterService : IItemCounterService
    {
        private readonly List<string> _supportedDataTypes = new()
        {
            "string", "integer", "double", "character", "boolean", "datetime"
        };

        public List<string> GetSupportedDataTypes() => _supportedDataTypes;

        public CountResponse CountItems(CountRequest request)
        {
            try
            {
                if (request.Items == null || !request.Items.Any())
                {
                    return new CountResponse
                    {
                        Success = false,
                        Message = "No items provided for counting."
                    };
                }

                if (!_supportedDataTypes.Contains(request.DataType.ToLower()))
                {
                    return new CountResponse
                    {
                        Success = false,
                        Message = $"Unsupported data type: {request.DataType}. Supported types: {string.Join(", ", _supportedDataTypes)}"
                    };
                }

                var counts = request.DataType.ToLower() switch
                {
                    "string" => CountStrings(request.Items),
                    "integer" => CountIntegers(request.Items),
                    "double" => CountDoubles(request.Items),
                    "character" => CountCharacters(request.Items),
                    "boolean" => CountBooleans(request.Items),
                    "datetime" => CountDates(request.Items),
                    _ => throw new ArgumentException($"Unsupported data type: {request.DataType}")
                };

                return new CountResponse
                {
                    Success = true,
                    Message = "Items counted successfully.",
                    Counts = counts,
                    DataType = request.DataType,
                    TotalItems = request.Items.Count
                };
            }
            catch (Exception ex)
            {
                return new CountResponse
                {
                    Success = false,
                    Message = $"Error processing request: {ex.Message}"
                };
            }
        }

        private Dictionary<string, int> CountStrings(List<string> items)
        {
            return items.GroupBy(item => item)
                       .ToDictionary(group => group.Key, group => group.Count());
        }

        private Dictionary<string, int> CountIntegers(List<string> items)
        {
            var integers = items.Select(item =>
            {
                if (!int.TryParse(item, out int result))
                    throw new FormatException($"'{item}' is not a valid integer.");
                return result;
            }).ToList();

            return integers.GroupBy(item => item)
                          .ToDictionary(group => group.Key.ToString(), group => group.Count());
        }

        private Dictionary<string, int> CountDoubles(List<string> items)
        {
            var doubles = items.Select(item =>
            {
                if (!double.TryParse(item, out double result))
                    throw new FormatException($"'{item}' is not a valid double.");
                return result;
            }).ToList();

            return doubles.GroupBy(item => item)
                         .ToDictionary(group => group.Key.ToString(), group => group.Count());
        }

        private Dictionary<string, int> CountCharacters(List<string> items)
        {
            var allChars = string.Join("", items).ToCharArray();
            return allChars.GroupBy(c => c)
                          .ToDictionary(group => group.Key.ToString(), group => group.Count());
        }

        private Dictionary<string, int> CountBooleans(List<string> items)
        {
            var booleans = items.Select(item =>
            {
                string lower = item.ToLower().Trim();
                return lower switch
                {
                    "true" or "yes" or "1" => true,
                    "false" or "no" or "0" => false,
                    _ => throw new FormatException($"'{item}' is not a valid boolean value.")
                };
            }).ToList();

            return booleans.GroupBy(item => item)
                          .ToDictionary(group => group.Key.ToString(), group => group.Count());
        }

        private Dictionary<string, int> CountDates(List<string> items)
        {
            var dates = items.Select(item =>
            {
                if (!DateTime.TryParse(item, out DateTime result))
                    throw new FormatException($"'{item}' is not a valid date.");
                return result.Date; // Only date part for counting
            }).ToList();

            return dates.GroupBy(item => item)
                       .ToDictionary(group => group.Key.ToString("yyyy-MM-dd"), group => group.Count());
        }
    }

    // Controllers
    [ApiController]
    [Route("api/[controller]")]
    public class ItemCounterController : ControllerBase
    {
        private readonly IItemCounterService _itemCounterService;

        public ItemCounterController(IItemCounterService itemCounterService)
        {
            _itemCounterService = itemCounterService;
        }

        /// <summary>
        /// Get API information and supported data types
        /// </summary>
        [HttpGet]
        public ActionResult<ApiResponse<object>> GetInfo()
        {
            var info = new
            {
                Name = "Item Counter API",
                Version = "1.0.0",
                Author = "Dumisani Nxumalo",
                Description = "API for counting occurrences of items across multiple data types",
                SupportedDataTypes = _itemCounterService.GetSupportedDataTypes(),
                Examples = new
                {
                    String = new { Items = new[] { "apple", "banana", "apple" }, DataType = "string" },
                    Integer = new { Items = new[] { "1", "2", "1", "3" }, DataType = "integer" },
                    Boolean = new { Items = new[] { "true", "false", "yes", "no" }, DataType = "boolean" }
                }
            };

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Item Counter API is running successfully.",
                Data = info
            });
        }

        /// <summary>
        /// Count occurrences of items in a list
        /// </summary>
        [HttpPost("count")]
        public ActionResult<ApiResponse<CountResponse>> CountItems([FromBody] CountRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<CountResponse>
                {
                    Success = false,
                    Message = "Invalid request data.",
                    Data = null
                });
            }

            var result = _itemCounterService.CountItems(request);

            if (!result.Success)
            {
                return BadRequest(new ApiResponse<CountResponse>
                {
                    Success = false,
                    Message = result.Message,
                    Data = result
                });
            }

            return Ok(new ApiResponse<CountResponse>
            {
                Success = true,
                Message = "Items counted successfully.",
                Data = result
            });
        }

        /// <summary>
        /// Get supported data types
        /// </summary>
        [HttpGet("datatypes")]
        public ActionResult<ApiResponse<List<string>>> GetSupportedDataTypes()
        {
            return Ok(new ApiResponse<List<string>>
            {
                Success = true,
                Message = "Supported data types retrieved successfully.",
                Data = _itemCounterService.GetSupportedDataTypes()
            });
        }

        /// <summary>
        /// Health check endpoint
        /// </summary>
        [HttpGet("health")]
        public ActionResult<ApiResponse<object>> HealthCheck()
        {
            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "API is healthy and running.",
                Data = new { Status = "Healthy", Uptime = DateTime.UtcNow }
            });
        }
    }
}
