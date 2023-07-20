using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBGList.Data;
using MyBGList.DTO;
using MyBGList.Models;
using System;
using System.Security.AccessControl;
using System.Xml.Linq;

namespace MyBGList.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]

    public class BoardGamesController : ControllerBase
    {
        private readonly ILogger<BoardGamesController> _logger;

        private readonly ApplicationDbContext _context;
        public BoardGamesController(
            ILogger<BoardGamesController> logger,
            ApplicationDbContext context
            )
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("GetBoarGameInforamtionFormDatabase")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public async Task<RestDTO<BoardGame[]>> GetBoarGameInforamtion()
        {
            var query = _context.BoardGames;
            return new RestDTO<BoardGame[]>()
            {
                Data = await query.ToArrayAsync(),
                Links = new List<LinkDTO> {
                            new LinkDTO(
                            Url.Action(null, "BoardGames",
                            null, Request.Scheme)!,
                            "self",
                            "GET"),
                }
            };

        }




        [HttpGet(Name = "GetBoardGames")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        public IEnumerable<BoardGame> GetGame()
        {
            return new[]
            {
                            new BoardGame() {
                            Id = 1,
                            Name = "Axis & Allies",
                            Year = 1981
                            },
                            new BoardGame() {
                            Id = 2,
                            Name = "Citadels",
                            Year = 2000
                            },
                            new BoardGame() {
                            Id = 3,
                            Name = "Terraforming Mars",
                            Year = 2016
                            }
            };
        }

        [HttpGet("BorderGameGeneric")]
        public RestDTO<BoardGame[]> GetBoorderGameGeneric()
        {
            return new RestDTO<BoardGame[]>()
            {
                Data = new BoardGame[] {
                new BoardGame()
                        {
                        Id = 1,
                        Name = "Axis & Allies",
                        Year = 1981
                },
                new BoardGame()
                        {
                        Id = 2,
                        Name = "Citadels",
                        Year = 2000
                },
                new BoardGame()
                        {
                            Id = 3,
                            Name = "Terraforming Mars",
                            Year = 2016
                       }
                    },
                Links = new List<LinkDTO> {
                new LinkDTO(
                Url.Action(null, "BoardGames", null, Request.Scheme)!,
                "self",
                "GET"),
                }
            };
        }

        [HttpPost(Name = "UpdateBoardGame")]
        [ResponseCache(NoStore = true)]
        public async Task<RestDTO<BoardGame?>> Post(BoardGameDTO model)
        {
            var boardgame = await _context.BoardGames
            .Where(b => b.Id == model.Id)
            .FirstOrDefaultAsync();

            if (boardgame != null)
            {
                if (!string.IsNullOrEmpty(model.Name))
                    boardgame.Name = model.Name;

                if (model.Year.HasValue && model.Year.Value > 0)
                    boardgame.Year = model.Year.Value;

                boardgame.LastModifiedDate = DateTime.Now;
                _context.BoardGames.Update(boardgame);
                await _context.SaveChangesAsync();
            };
            return new RestDTO<BoardGame?>()
            {
                Data = boardgame,
                Links = new List<LinkDTO>
            {
                    new LinkDTO(

                    Url.Action( null,   "BoardGames",   model, Request.Scheme)!,
                    "self",
                    "POST"),
                    }
            };
        }


        [HttpDelete(Name = "DeleteBoardGame")]
        [ResponseCache(NoStore = true)]
        public async Task<RestDTO<BoardGame?>> Delete(int id)
        {
            var boardgame = await _context.BoardGames
            .Where(b => b.Id == id)
            .FirstOrDefaultAsync();
            if (boardgame != null)
            {
                _context.BoardGames.Remove(boardgame);
                await _context.SaveChangesAsync();
            };
            return new RestDTO<BoardGame?>()
            {
                Data = boardgame,
                Links = new List<LinkDTO>
                {
                new LinkDTO(
                Url.Action(
                null,
                "BoardGames",
                id,
                Request.Scheme)!,
                "self",
                "DELETE"),
                }
            };
        }

    }
}
