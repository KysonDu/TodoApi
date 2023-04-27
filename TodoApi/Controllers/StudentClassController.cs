using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    //如果访问的路径为404则需要配置路由
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StudentClassController : ControllerBase
    {
        private readonly TodoContext _context;

        public StudentClassController(TodoContext context)
        {
            _context = context;
            if (_context.StudentClasses.Count() == 0)
            {
                // 如果集合为空，则创建新的 StudentClass，
                _context.StudentClasses.Add(new StudentClass { Name = "一班"});
                _context.StudentClasses.Add(new StudentClass { Name = "二班" });
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// 查询所有列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public async Task<IEnumerable<StudentClass>> GetAll()
        {
            return await _context.StudentClasses.ToArrayAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="baseQuery"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<StudentClass>> GetPagination([FromQuery] BaseQuery baseQuery)
        {
            IEnumerable<StudentClass> list = await _context.StudentClasses.ToArrayAsync();

            if (baseQuery.Key == null)
            {
                return list.OrderByDescending(o => o.Id).Distinct().Skip((baseQuery.Page - 1) * baseQuery.Size).Take(baseQuery.Size);
            }

            return list.Where(o => o.Name.Contains(baseQuery.Key)).OrderByDescending(o => o.Id).Distinct().Skip((baseQuery.Page - 1) * baseQuery.Size).Take(baseQuery.Size);
        }

        /// <summary>
        /// 根据Id查找数据
        /// </summary>
        /// <param name="Id">序号</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<StudentClass> GetByID(int Id)
        {
            var item = await _context.StudentClasses.FirstOrDefaultAsync(t => t.Id == Id);
            return item;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentClass item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            else
            {
                _context.StudentClasses.Add(item);
                await _context.SaveChangesAsync();
                return Ok(item);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentClass item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            else
            {
                var studentClass = _context.StudentClasses.SingleOrDefault(t => t.Id == id);

                if (studentClass == null)
                {
                    return NotFound();
                }
                else
                {
                    studentClass.Name = item.Name;

                    _context.StudentClasses.Update(studentClass);
                    await _context.SaveChangesAsync();

                    return Ok(studentClass);
                }

            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var studentClass = _context.StudentClasses.SingleOrDefault(t => t.Id == Id);

            if (studentClass == null)
            {

                return NotFound();
            }
            else
            {
                _context.StudentClasses.Remove(studentClass);
                await _context.SaveChangesAsync();

                return Ok();
            }
        }
    }
}
