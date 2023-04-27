using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Models.ApiResult;
using TodoApi.Models.DTO;

namespace TodoApi.Controllers
{
    //如果访问的路径为404则需要配置路由
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        private readonly IMapper _mapper;

        public TodoController(TodoContext context, IMapper mapper)
        {
            _context = context;
            if (_context.TodoItems.Count() == 0)
            {
                // 如果集合为空，则创建新的 TodoItem，
                // which means you can't delete all TodoItems. _context.TodoItems.Add(new TodoItem { Name = "Item1" }); _context.SaveChanges(); 
                _context.TodoItems.Add(new TodoItem { Name = "Item1", IsComplete = false });
                _context.TodoItems.Add(new TodoItem { Name = "Item2", IsComplete = false });
                _context.TodoItems.Add(new TodoItem { Name = "Item3", IsComplete = true });

                _context.SaveChanges();
            }
            _mapper = mapper;
        }

        /// <summary>
        /// 查询所有列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult<ApiResult>> GetAll()
        {
            //int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            return ApiResultHelper.Success(await _context.TodoItems.ToArrayAsync());
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="baseQuery"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ApiResult>> GetPagination([FromQuery] BaseQuery baseQuery)
        {
            IEnumerable<TodoItem> list = await _context.TodoItems.ToArrayAsync();

            if (baseQuery.Key == null)
            {
                return ApiResultHelper.Success(list.OrderByDescending(o => o.Id).Distinct().Skip((baseQuery.Page - 1) * baseQuery.Size).Take(baseQuery.Size), list.Count());
            }

            return ApiResultHelper.Success(list.Where(o => o.Name.Contains(baseQuery.Key)).OrderByDescending(o => o.Id).Distinct().Skip((baseQuery.Page - 1) * baseQuery.Size).Take(baseQuery.Size), list.Count());
        }

        /// <summary>
        /// 根据Id查找数据
        /// </summary>
        /// <param name="Id">序号</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<ActionResult<ApiResult>> GetByID(int Id)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(t => t.Id == Id);
            var todoItemDTO = _mapper.Map<TodoItemDto>(item);
            return ApiResultHelper.Success(todoItemDTO);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ApiResult>> Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            else
            {
                _context.TodoItems.Add(item);
                await _context.SaveChangesAsync();
                return ApiResultHelper.Success(item);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResult>> Update(int id, [FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            else
            {
                var todo = _context.TodoItems.SingleOrDefault(t => t.Id == id);

                if (todo == null)
                {
                    return NotFound();
                }
                else
                {
                    todo.IsComplete = item.IsComplete;
                    todo.Name = item.Name;

                    _context.TodoItems.Update(todo);
                    await _context.SaveChangesAsync();

                    return ApiResultHelper.Success(todo);
                }

            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<ActionResult<ApiResult>> Delete(int Id)
        {
            var todo = _context.TodoItems.SingleOrDefault(t => t.Id == Id);

            if (todo == null)
            {

                return NotFound();
            }
            else
            {
                _context.TodoItems.Remove(todo);
                await _context.SaveChangesAsync();

                return ApiResultHelper.Success(todo);
            }
        }
    }
}
