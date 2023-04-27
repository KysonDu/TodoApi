using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Models.DTO;

namespace TodoApi.Controllers
{
    //如果访问的路径为404则需要配置路由
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StudentController : ControllerBase
    {
        private readonly TodoContext _context;

        private readonly IMapper _mapper;

        public StudentController(TodoContext context, IMapper mapper)
        {
            _context = context;
            if (_context.Students.Count() == 0)
            {
                // 如果集合为空，则创建新的 Student，
                _context.Students.Add(new Student { Name = "张三", Age = 18, Address = "北京", Gender = Gender.男, PhotoPath = "123456", StudentClassId = 1 });
                _context.Students.Add(new Student { Name = "李四", Age = 23, Address = "上海", Gender = Gender.女, PhotoPath = "123456", StudentClassId = 1 });
                _context.Students.Add(new Student { Name = "王五", Age = 13, Address = "广州", Gender = Gender.其他, PhotoPath = "123456", StudentClassId = 2 });
                _context.Students.Add(new Student { Name = "赵六", Age = 12, Address = "深圳", Gender = Gender.男, PhotoPath = "123456", StudentClassId = 1 });
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
        public async Task<IEnumerable<StudentDto>> GetAll()
        {
            IEnumerable<Student> list = await _context.Students.Include(c => c.StudentClass).ToArrayAsync();
            var newsDTO = _mapper.Map<List<StudentDto>>(list);
            return newsDTO;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="baseQuery"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<StudentDto>> GetPagination([FromQuery] BaseQuery baseQuery)
        {
            IEnumerable<Student> list = await _context.Students.Include(c => c.StudentClass).ToArrayAsync();

            if (baseQuery.Key == null)
            {
                return  _mapper.Map<List<StudentDto>>(list.OrderByDescending(o => o.Id).Distinct().Skip((baseQuery.Page - 1) * baseQuery.Size).Take(baseQuery.Size));
            }

            return _mapper.Map<List<StudentDto>>(list.Where(o => o.Name.Contains(baseQuery.Key)).OrderByDescending(o => o.Id).Distinct().Skip((baseQuery.Page - 1) * baseQuery.Size).Take(baseQuery.Size));
        }

        /// <summary>
        /// 根据Id查找数据
        /// </summary>
        /// <param name="Id">序号</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<StudentDto> GetByID(int Id)
        {
            var item = await _context.Students.Include(c => c.StudentClass).FirstOrDefaultAsync(t => t.Id == Id);
            var todoItemDTO = _mapper.Map<StudentDto>(item);
            return todoItemDTO;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            else
            {
                _context.Students.Add(item);
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
        public async Task<IActionResult> Update(int id, [FromBody] Student item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            else
            {
                var student = _context.Students.SingleOrDefault(t => t.Id == id);

                if (student == null)
                {
                    return NotFound();
                }
                else
                {
                    student.Name = item.Name;

                    _context.Students.Update(student);
                    await _context.SaveChangesAsync();

                    return Ok(student);
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
            var student = _context.Students.SingleOrDefault(t => t.Id == Id);

            if (student == null)
            {

                return NotFound();
            }
            else
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                return Ok();
            }
        }
    }
}
