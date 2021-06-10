using System.Threading.Tasks;
using Common.Thesaurus.Dto;
using Common.Thesaurus.Helpers.ExceptionHelper;
using Common.Thesaurus.Interfaces.Logger;
using Common.Thesaurus.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Thesaurus.Controllers
{
    public class WordsController : Controller
    {
        private readonly IWordService _wordService;
        private readonly ILoggerManager _logger;

        public WordsController(IWordService wordService, ILoggerManager logger)
        {
            _wordService = wordService;
            _logger = logger;
        }

        // GET: Words
        public async Task<IActionResult> Index([FromQuery] string searchString = null)
        {
            var words = searchString != null ?
                await _wordService.GetFiltered(searchString) :
                await _wordService.GetAll();

            return View(words);
        }

        // GET: Words/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                _logger.LogWarn("Word Id with null value was passed - Details action");
                return NotFound();
            }

            var word = await _wordService.Get(id: id.Value);
            if (word == null)
            {
                _logger.LogWarn("Word with id: {0} not found in db.");
                return NotFound();
            }

            return View(word);
        }

        // GET: Words/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Words/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Synonyms")] WordDto wordDto)
        {
            if (ModelState.IsValid)
            {
                await _wordService.Create(wordDto);
                return RedirectToAction(nameof(Index));
            }

            return View(wordDto);
        }

        // GET: Words/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                _logger.LogWarn("Word Id with null value was passed - Edit action");
                return NotFound();
            }

            var word = await _wordService.Get(id.Value);
            
            if (word == null)
            {
                _logger.LogWarn("Word with id: {0} not found in db.");
                return NotFound();
            }

            return View(word);
        }

        // POST: Words/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Synonyms")] WordDto wordDto)
        {
            if (id != wordDto.Id)
            {
                _logger.LogWarn(string.Format("Id({0}) differs from Id{1} in dto  - Edit action", id, wordDto.Id));
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                try
                {
                    await _wordService.Update(wordDto);
                }
                catch (DbUpdateConcurrencyException exception)
                {
                    _logger.LogError(exception.GetMessage());

                    if (!await WordExists(wordDto.Id))
                    {
                        _logger.LogWarn("Word Id with null value was passed - Details action");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(wordDto);
        }

        // GET: Words/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                _logger.LogWarn("Word Id with null value was passed - Delete action");
                return NotFound();
            }

            var word = await _wordService.Get(id.Value);
            if (word == null)
            {
                _logger.LogWarn("Word with id: {0} not found in db. - Delete action");
                return NotFound();
            }

            return View(word);
        }

        // POST: Words/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _wordService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> WordExists(int id)
        {
            return await _wordService.Exists(id);
        }
    }
}
