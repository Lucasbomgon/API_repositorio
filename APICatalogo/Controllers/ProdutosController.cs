using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;


namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            return await _context.produtos.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await _context.produtos.AsNoTracking().FirstOrDefaultAsync(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound(); // usar ActionResult<> para funcionar NotFound... 
            }
            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest();

            _context.produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult delete(int id)
        {
            var produto = _context.produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto nao localizado...");
            }

            _context.produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }

        public override bool Equals(object? obj)
        {
            return obj is ProdutosController controller &&
                   EqualityComparer<HttpContext>.Default.Equals(HttpContext, controller.HttpContext) &&
                   EqualityComparer<HttpRequest>.Default.Equals(Request, controller.Request) &&
                   EqualityComparer<HttpResponse>.Default.Equals(Response, controller.Response) &&
                   EqualityComparer<RouteData>.Default.Equals(RouteData, controller.RouteData) &&
                   EqualityComparer<ModelStateDictionary>.Default.Equals(ModelState, controller.ModelState) &&
                   EqualityComparer<ControllerContext>.Default.Equals(ControllerContext, controller.ControllerContext) &&
                   EqualityComparer<IModelMetadataProvider>.Default.Equals(MetadataProvider, controller.MetadataProvider) &&
                   EqualityComparer<IModelBinderFactory>.Default.Equals(ModelBinderFactory, controller.ModelBinderFactory) &&
                   EqualityComparer<IUrlHelper>.Default.Equals(Url, controller.Url) &&
                   EqualityComparer<IObjectModelValidator>.Default.Equals(ObjectValidator, controller.ObjectValidator) &&
                   EqualityComparer<ProblemDetailsFactory>.Default.Equals(ProblemDetailsFactory, controller.ProblemDetailsFactory) &&
                   EqualityComparer<ClaimsPrincipal>.Default.Equals(User, controller.User) &&
                   EqualityComparer<AppDbContext>.Default.Equals(_context, controller._context);
        }
    }

    public class asyn
    {
    }
}
    
