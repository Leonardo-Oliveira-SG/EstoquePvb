using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Queries;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Queries
{
    public class BuscaQuery : IBuscaQuery
    {
        private readonly IEstoqueTemporarioRepository _estoqueTemporarioRepository;
        private readonly IPvbRepository _pvbRepository;
        private readonly IEstoqueFabricanteRepository _estoqueFabricanteRepository;
        private readonly IMovimentacaoPvbRepository _movimentacaoRepository;
        private readonly IEstoquePvbRepository _estoquePvbRepository;

        public BuscaQuery(
            IEstoqueTemporarioRepository estoqueTemporarioRepository,
            IEstoquePvbRepository estoquePvbRepository,
            IPvbRepository pvbRepository,
            IEstoqueFabricanteRepository estoqueFabricanteRepository,
            IMovimentacaoPvbRepository movimentacaoRepository)
        {
            _estoqueTemporarioRepository = estoqueTemporarioRepository;
            _estoquePvbRepository = estoquePvbRepository;
            _pvbRepository = pvbRepository;
            _estoqueFabricanteRepository = estoqueFabricanteRepository;
            _movimentacaoRepository = movimentacaoRepository;
        }

        public async Task<List<BuscaEstoqueDto>> GetBuscaEstoqueAsync()
        {
            var estoqueQuery = from ep in _estoquePvbRepository.EstoquePvb
                               join p in _pvbRepository.Pvb on ep.Codigo equals p.Codigo
                               select new BuscaEstoqueDto
                               {
                                   Codigo = ep.Codigo,
                                   TipoPvb = p.TipoPvb,
                                   CodigoPvb = p.CodigoPvb,
                                   Fabricante = p.Fabricante,
                                   Espessura = p.Espessura,
                                   TamanhoRolo = p.TamanhoRolo,
                                   Saldo = ep.Saldo,
                               };


            var estoque = await estoqueQuery
                .OrderBy(ep => ep.Codigo)
                .ThenBy(ep => ep.TamanhoRolo)
                .ToListAsync();

            return estoque;

        }

        public async Task<List<BuscaEstoqueFabricanteDto>> GetBuscaEstoqueFabricanteAsync()
        {
            var estoqueQuery = from ef in _estoqueFabricanteRepository.EstoqueFabricante
                               join p in _pvbRepository.Pvb on ef.Codigo equals p.Codigo
                               select new BuscaEstoqueFabricanteDto
                               {
                                   Codigo = ef.Codigo,
                                   TipoPvb = p.TipoPvb,
                                   CodigoPvb = p.CodigoPvb,
                                   Fabricante = ef.Fabricante,
                                   Espessura = p.Espessura,
                                   TamanhoRolo = p.TamanhoRolo,
                                   Saldo = ef.Saldo,
                               };


            var estoqueFabricante = await estoqueQuery
                .OrderBy(ef => ef.Codigo)
                .ToListAsync();

            return estoqueFabricante;

        }

        public async Task<List<BuscaEstoqueTemporarioDto>> GetBuscaEstoqueTemporarioAsync()
        {
            var estoqueQuery = from et in _estoqueTemporarioRepository.EstoqueTemporario
                               join p in _pvbRepository.Pvb on et.Codigo equals p.Codigo
                               select new BuscaEstoqueTemporarioDto
                               {
                                   Codigo = et.Codigo,
                                   TipoPvb = p.TipoPvb,
                                   CodigoPvb = p.CodigoPvb,
                                   Fabricante = et.Fabricante,
                                   Espessura = p.Espessura,
                                   TamanhoRolo = p.TamanhoRolo,
                                   NotaFiscal = et.NotaFiscal,
                                   NumeroRolo = et.NumeroRolo,
                                   Saldo = et.Saldo,
                               };


            var estoqueTemporario = await estoqueQuery
                .Where(et => et.Saldo > 0)
                .OrderBy(et => et.Codigo)
                .ToListAsync();

            return estoqueTemporario;

        }

        public async Task<PaginationResponse<BuscaMovimentacaoPvbDto>> GetBuscaMovimentacaoPvbAsync(PaginationRequest paginationRequest)
        {
            var totalItems = await (from mv in _movimentacaoRepository.MovimentacaoPvb
                                    select mv).CountAsync();

            var skip = (paginationRequest.PageNumber - 1) * paginationRequest.PageSize;

            var movimentacaoQuery = from mv in _movimentacaoRepository.MovimentacaoPvb
                                    join tp in _pvbRepository.Pvb on mv.Codigo equals tp.Codigo
                                    join et in _estoqueTemporarioRepository.EstoqueTemporario on mv.NumeroRolo equals et.NumeroRolo
                                    select new BuscaMovimentacaoPvbDto
                                    {
                                        Codigo = mv.Codigo,
                                        TipoPvb = tp.TipoPvb,
                                        Fabricante = et.Fabricante,
                                        CodigoPvb = tp.CodigoPvb,
                                        Espessura = tp.Espessura,
                                        TamanhoRolo = tp.TamanhoRolo,
                                        NotaFiscal = et.NotaFiscal,
                                        NumeroRolo = mv.NumeroRolo,
                                        Destino = mv.Destino,
                                        Data = mv.Data,

                                    };


            var movimentacao = await movimentacaoQuery
            .OrderBy(mv => mv.Data)
            .Skip(skip)
            .Take(paginationRequest.PageSize)
            .ToListAsync();

            return new PaginationResponse<BuscaMovimentacaoPvbDto>
            {
                PageNumber = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize,
                TotalItems = totalItems,
                Data = movimentacao
            };
        }

        public async Task<List<CoberturaDeEstoqueDto>> GetBuscaCoberturaDeEstoqueAsync()
        {
            var buscaCoberturaDeEstoque = from m in _movimentacaoRepository.MovimentacaoPvb
                                          join l in _pvbRepository.Pvb on m.Codigo equals l.Codigo
                                          where m.Data >= DateTime.Now.AddDays(-90) && m.Destino != "Estoque"
                                          group m by new { m.Codigo, l.TipoPvb, l.CodigoPvb, l.TamanhoRolo, l.Espessura } into g
                                          select new CoberturaDeEstoqueDto
                                          {
                                              Codigo = g.Key.Codigo,
                                              TipoPvb = g.Key.TipoPvb,
                                              CodigoPvb = g.Key.CodigoPvb,
                                              TamanhoRolo = g.Key.TamanhoRolo,
                                              Espessura = g.Key.Espessura,
                                              TotalUsado90Dias = g.Count(),
                                              MediaConsumo = g.Count() / 3m,
                                          };

            var buscaCoberturaResult = await buscaCoberturaDeEstoque.ToListAsync();

            var estoqueReferente = from e in _estoquePvbRepository.EstoquePvb
                                   where buscaCoberturaResult.Select(b => b.Codigo).Contains(e.Codigo)
                                   select new CoberturaDeEstoqueDto
                                   {
                                       Codigo = e.Codigo,
                                       EstoqueReferente = e.Saldo
                                   };

            var estoqueResult = await estoqueReferente.ToListAsync();

            var buscaUsadoUltimos30Dias = from m in _movimentacaoRepository.MovimentacaoPvb
                                          join l in _pvbRepository.Pvb on m.Codigo equals l.Codigo
                                          where m.Data >= DateTime.Now.AddDays(-30) && m.Destino != "Estoque"
                                          group m by new { m.Codigo } into g
                                          select new CoberturaDeEstoqueDto
                                          {
                                              Codigo = g.Key.Codigo,
                                              TotalUsado30Dias = g.Count(),
                                          };

            var buscaUsadoUltimos30DiasResult = await buscaUsadoUltimos30Dias.ToListAsync();

            var resultado = buscaCoberturaResult.Select(b => new CoberturaDeEstoqueDto
            {
                Codigo = b.Codigo,
                TipoPvb = b.TipoPvb,
                CodigoPvb = b.CodigoPvb,
                TamanhoRolo = b.TamanhoRolo,
                Espessura = b.Espessura,
                TotalUsado90Dias = b.TotalUsado90Dias,
                MediaConsumo = Math.Round(b.MediaConsumo, 2), // Arredonda para 2 dígitos
                EstoqueReferente = estoqueResult.FirstOrDefault(e => e.Codigo == b.Codigo)?.EstoqueReferente ?? 0,
                CoberturaEstoque = b.MediaConsumo > 0
                ? (int)(estoqueResult.FirstOrDefault(e => e.Codigo == b.Codigo)!.EstoqueReferente / (b.MediaConsumo / (decimal)30.44)) : 0,
                TotalUsado30Dias = buscaUsadoUltimos30DiasResult.FirstOrDefault(bu => bu.Codigo == b.Codigo)?.TotalUsado30Dias ?? 0
            }).
            OrderBy(g => g.Espessura).ToList();

            return resultado;
        }

        public async Task<List<CoberturaDeEstoqueDto>> GetBuscaCoberturaDeEstoquePorEspessuraAsync(decimal espessura)
        {
            var buscaCoberturaDeEstoque = from m in _movimentacaoRepository.MovimentacaoPvb
                                          join l in _pvbRepository.Pvb on m.Codigo equals l.Codigo
                                          where m.Data >= DateTime.Now.AddDays(-90) && m.Destino != "Estoque" && l.Espessura == espessura
                                          group m by new { m.Codigo, l.TipoPvb, l.CodigoPvb, l.TamanhoRolo, l.Espessura } into g
                                          select new CoberturaDeEstoqueDto
                                          {
                                              Codigo = g.Key.Codigo,
                                              TipoPvb = g.Key.TipoPvb,
                                              CodigoPvb = g.Key.CodigoPvb,
                                              TamanhoRolo = g.Key.TamanhoRolo,
                                              Espessura = g.Key.Espessura,
                                              TotalUsado90Dias = g.Count(),
                                              MediaConsumo = g.Count() / 3m,
                                          };

            var buscaCoberturaResult = await buscaCoberturaDeEstoque.ToListAsync();

            var estoqueReferente = from e in _estoquePvbRepository.EstoquePvb
                                   where buscaCoberturaResult.Select(b => b.Codigo).Contains(e.Codigo)
                                   select new CoberturaDeEstoqueDto
                                   {
                                       Codigo = e.Codigo,
                                       EstoqueReferente = e.Saldo
                                   };

            var estoqueResult = await estoqueReferente.ToListAsync();

            var buscaUsadoUltimos30Dias = from m in _movimentacaoRepository.MovimentacaoPvb
                                          join l in _pvbRepository.Pvb on m.Codigo equals l.Codigo
                                          where m.Data >= DateTime.Now.AddDays(-30) && m.Destino != "Estoque" && l.Espessura == espessura
                                          group m by new { m.Codigo } into g
                                          select new CoberturaDeEstoqueDto
                                          {
                                              Codigo = g.Key.Codigo,
                                              TotalUsado30Dias = g.Count(),
                                          };

            var buscaUsadoUltimos30DiasResult = await buscaUsadoUltimos30Dias.ToListAsync();

            var resultado = buscaCoberturaResult.Select(b => new CoberturaDeEstoqueDto
            {
                Codigo = b.Codigo,
                TipoPvb = b.TipoPvb,
                CodigoPvb = b.CodigoPvb,
                TamanhoRolo = b.TamanhoRolo,
                Espessura = b.Espessura,
                TotalUsado90Dias = b.TotalUsado90Dias,
                MediaConsumo = Math.Round(b.MediaConsumo, 2), // Arredonda para 2 dígitos
                EstoqueReferente = estoqueResult.FirstOrDefault(e => e.Codigo == b.Codigo)?.EstoqueReferente ?? 0,
                CoberturaEstoque = b.MediaConsumo > 0
                ? (int)(estoqueResult.FirstOrDefault(e => e.Codigo == b.Codigo)!.EstoqueReferente / (b.MediaConsumo / (decimal)30.44)) : 0,
                TotalUsado30Dias = buscaUsadoUltimos30DiasResult.FirstOrDefault(bu => bu.Codigo == b.Codigo)?.TotalUsado30Dias ?? 0
            }).
                OrderBy(g => g.Espessura).ToList();

            return resultado;
        }

        public async Task<List<CoberturaDeEstoqueDto>> GetBuscaCoberturaDeEstoquePorDestinoAsync(string destino)
        {
            var buscaCoberturaDeEstoque = from m in _movimentacaoRepository.MovimentacaoPvb
                                          join l in _pvbRepository.Pvb on m.Codigo equals l.Codigo
                                          where m.Data >= DateTime.Now.AddDays(-90) && m.Destino == destino
                                          group m by new { m.Codigo, l.TipoPvb, l.CodigoPvb, l.TamanhoRolo, l.Espessura } into g
                                          select new CoberturaDeEstoqueDto
                                          {
                                              Codigo = g.Key.Codigo,
                                              TipoPvb = g.Key.TipoPvb,
                                              CodigoPvb = g.Key.CodigoPvb,
                                              TamanhoRolo = g.Key.TamanhoRolo,
                                              Espessura = g.Key.Espessura,
                                              TotalUsado90Dias = g.Count(),
                                              MediaConsumo = g.Count() / 3m,
                                          };

            var buscaCoberturaResult = await buscaCoberturaDeEstoque.ToListAsync();

            var estoqueReferente = from e in _estoquePvbRepository.EstoquePvb
                                   where buscaCoberturaResult.Select(b => b.Codigo).Contains(e.Codigo)
                                   select new CoberturaDeEstoqueDto
                                   {
                                       Codigo = e.Codigo,
                                       EstoqueReferente = e.Saldo
                                   };

            var estoqueResult = await estoqueReferente.ToListAsync();

            var buscaUsadoUltimos30Dias = from m in _movimentacaoRepository.MovimentacaoPvb
                                          join l in _pvbRepository.Pvb on m.Codigo equals l.Codigo
                                          where m.Data >= DateTime.Now.AddDays(-30) && m.Destino == destino
                                          group m by new { m.Codigo } into g
                                          select new CoberturaDeEstoqueDto
                                          {
                                              Codigo = g.Key.Codigo,
                                              TotalUsado30Dias = g.Count(),
                                          };

            var buscaUsadoUltimos30DiasResult = await buscaUsadoUltimos30Dias.ToListAsync();

            var resultado = buscaCoberturaResult.Select(b => new CoberturaDeEstoqueDto
            {
                Codigo = b.Codigo,
                TipoPvb = b.TipoPvb,
                CodigoPvb = b.CodigoPvb,
                TamanhoRolo = b.TamanhoRolo,
                Espessura = b.Espessura,
                TotalUsado90Dias = b.TotalUsado90Dias,
                MediaConsumo = Math.Round(b.MediaConsumo, 2), // Arredonda para 2 dígitos
                EstoqueReferente = estoqueResult.FirstOrDefault(e => e.Codigo == b.Codigo)?.EstoqueReferente ?? 0,
                CoberturaEstoque = b.MediaConsumo > 0
                ? (int)(estoqueResult.FirstOrDefault(e => e.Codigo == b.Codigo)!.EstoqueReferente / (b.MediaConsumo / (decimal)30.44)) : 0,
                TotalUsado30Dias = buscaUsadoUltimos30DiasResult.FirstOrDefault(bu => bu.Codigo == b.Codigo)?.TotalUsado30Dias ?? 0
            }).
                OrderBy(g => g.Espessura).ToList();

            return resultado;
        }
        public async Task<List<CoberturaDeEstoqueDto>> GetBuscaCoberturaDeEstoquePorEspessuraAndDestinoAsync(decimal espessura, string destino)
        {
            var buscaCoberturaDeEstoque = from m in _movimentacaoRepository.MovimentacaoPvb
                                          join l in _pvbRepository.Pvb on m.Codigo equals l.Codigo
                                          where m.Data >= DateTime.Now.AddDays(-90) && m.Destino == destino && l.Espessura == espessura
                                          group m by new { m.Codigo, l.TipoPvb, l.CodigoPvb, l.TamanhoRolo, l.Espessura } into g
                                          select new CoberturaDeEstoqueDto
                                          {
                                              Codigo = g.Key.Codigo,
                                              TipoPvb = g.Key.TipoPvb,
                                              CodigoPvb = g.Key.CodigoPvb,
                                              TamanhoRolo = g.Key.TamanhoRolo,
                                              Espessura = g.Key.Espessura,
                                              TotalUsado90Dias = g.Count(),
                                              MediaConsumo = g.Count() / 3m,
                                          };

            var buscaCoberturaResult = await buscaCoberturaDeEstoque.ToListAsync();

            var estoqueReferente = from e in _estoquePvbRepository.EstoquePvb
                                   where buscaCoberturaResult.Select(b => b.Codigo).Contains(e.Codigo)
                                   select new CoberturaDeEstoqueDto
                                   {
                                       Codigo = e.Codigo,
                                       EstoqueReferente = e.Saldo
                                   };

            var estoqueResult = await estoqueReferente.ToListAsync();

            var buscaUsadoUltimos30Dias = from m in _movimentacaoRepository.MovimentacaoPvb
                                          join l in _pvbRepository.Pvb on m.Codigo equals l.Codigo
                                          where m.Data >= DateTime.Now.AddDays(-30) && m.Destino == destino && l.Espessura == espessura
                                          group m by new { m.Codigo } into g
                                          select new CoberturaDeEstoqueDto
                                          {
                                              Codigo = g.Key.Codigo,
                                              TotalUsado30Dias = g.Count(),
                                          };

            var buscaUsadoUltimos30DiasResult = await buscaUsadoUltimos30Dias.ToListAsync();

            var resultado = buscaCoberturaResult.Select(b => new CoberturaDeEstoqueDto
            {
                Codigo = b.Codigo,
                TipoPvb = b.TipoPvb,
                CodigoPvb = b.CodigoPvb,
                TamanhoRolo = b.TamanhoRolo,
                Espessura = b.Espessura,
                TotalUsado90Dias = b.TotalUsado90Dias,
                MediaConsumo = Math.Round(b.MediaConsumo, 2), // Arredonda para 2 dígitos
                EstoqueReferente = estoqueResult.FirstOrDefault(e => e.Codigo == b.Codigo)?.EstoqueReferente ?? 0,
                CoberturaEstoque = b.MediaConsumo > 0
                ? (int)(estoqueResult.FirstOrDefault(e => e.Codigo == b.Codigo)!.EstoqueReferente / (b.MediaConsumo / (decimal)30.44)) : 0,
                TotalUsado30Dias = buscaUsadoUltimos30DiasResult.FirstOrDefault(bu => bu.Codigo == b.Codigo)?.TotalUsado30Dias ?? 0
            }).
                OrderBy(g => g.Espessura).ToList();

            return resultado;
        }
    }
}

