using Application.Interfaces;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

public class ControleEstoquePvbService : IEntradaRoloService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPvbRepository _pvbRepository;
    private readonly IEstoqueTemporarioRepository _estoqueTemporarioRepository;
    private readonly IEstoquePvbRepository _estoquePvbRepository;
    private readonly IEstoqueFabricanteRepository _estoqueFabricanteRepository;
    private readonly IMovimentacaoPvbRepository _movimentacaoRepository;

    public ControleEstoquePvbService(IUnitOfWork unitOfWork,
        IEstoqueTemporarioRepository estoqueTemporarioRepository,
        IEstoquePvbRepository estoquePvbRepository,
        IEstoqueFabricanteRepository estoqueFabricanteRepository,
        IMovimentacaoPvbRepository movimentacaoRepository, IPvbRepository pvbRepository)
    {
        _unitOfWork = unitOfWork;
        _pvbRepository = pvbRepository;
        _estoqueTemporarioRepository = estoqueTemporarioRepository;
        _estoquePvbRepository = estoquePvbRepository;
        _estoqueFabricanteRepository = estoqueFabricanteRepository;
        _movimentacaoRepository = movimentacaoRepository;
    }

    public async Task<EntradaRoloDto> IncrementarEstoque(EntradaRoloDto entradaRoloDto)
    {
        if (await _pvbRepository.Exists(entradaRoloDto.Codigo))
        {
            try
            {
                if (await _estoqueTemporarioRepository.Exists(entradaRoloDto.NumeroRolo))
                {
                    throw new Exception("Este rolo já foi cadastrado.");
                }

                
                var estoqueTemporario = new EstoqueTemporario
                {
                    Codigo = entradaRoloDto.Codigo,
                    Fabricante = entradaRoloDto.Fabricante!,
                    NotaFiscal = entradaRoloDto.NotaFiscal!,
                    Saldo = 1,
                    NumeroRolo = entradaRoloDto.NumeroRolo
                };

                await _estoqueTemporarioRepository.AddAsync(estoqueTemporario);

                var estoquePvb = await _estoquePvbRepository.GetByCodigoAsync(entradaRoloDto.Codigo);

                if (estoquePvb == null)
                {
                    estoquePvb = new EstoquePvb
                    {
                        Codigo = entradaRoloDto.Codigo,
                        Saldo = 1, 
                    };

                    await _estoquePvbRepository.AddAsync(estoquePvb);
                }
                else
                {
                    estoquePvb.Saldo += 1;

                    await _estoquePvbRepository.Update(estoquePvb);
                }

                
                var estoqueFabricante = await _estoqueFabricanteRepository.GetByFabricanteAndCodigo(entradaRoloDto.Fabricante, entradaRoloDto.Codigo);
                if (estoqueFabricante == null)
                {
                    estoqueFabricante = new EstoqueFabricante
                    {
                        Id = Guid.NewGuid(),
                        Codigo = entradaRoloDto.Codigo,
                        Fabricante = entradaRoloDto.Fabricante,
                        Saldo = 1
                    };

                    await _estoqueFabricanteRepository.AddAsync(estoqueFabricante);

                }
                else
                {
                    estoqueFabricante.Saldo += 1;

                    await _estoqueFabricanteRepository.Update(estoqueFabricante);
                }

                
                var movimentacao = new MovimentacaoPvb
                {
                    Codigo = entradaRoloDto.Codigo,
                    Data = DateTime.Now,
                    NumeroRolo = entradaRoloDto.NumeroRolo,
                    Destino = "Estoque"
                };

                await _movimentacaoRepository.AddAsync(movimentacao);

                
                await _unitOfWork.SaveAsync();

                
                return entradaRoloDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao incrementar estoque", ex);
            }
        }
        else
        {
            throw new Exception("Este pvb não exixte na lista de PVB atual.");
        }
    }

    public async Task<SaidaRoloDto> BaixarEstoque(SaidaRoloDto saidaRoloDto)
    {
        if (!await _estoqueTemporarioRepository.Exists(saidaRoloDto.NumeroRolo!))
        {
            throw new Exception("Este rolo não está cadastrado no estoque.");
        }

        try
        {
            var estoqueTemporario = await _estoqueTemporarioRepository.GetByNumeroRoloAsync(saidaRoloDto.NumeroRolo!);

            if (estoqueTemporario.Saldo > 0)
            {
                estoqueTemporario.Saldo -= 1; 
                           
                await _estoqueTemporarioRepository.Update(estoqueTemporario);
            }
            else
            {
                throw new Exception("Saldo insuficiente.");
            }

            var estoquePvb = await _estoquePvbRepository.GetByCodigoAsync(saidaRoloDto.Codigo);

            if (estoquePvb != null && estoquePvb.Saldo > 0)
            {
                estoquePvb.Saldo -= 1; 
                
                await _estoquePvbRepository.Update(estoquePvb);
                
            }
            else
            {
                throw new Exception("Estoque PVB insuficiente.");
            }

            var estoqueFabricante = await _estoqueFabricanteRepository.GetByFabricanteAndCodigo(saidaRoloDto.Fabricante, saidaRoloDto.Codigo);
            if (estoqueFabricante != null && estoqueFabricante.Saldo > 0)
            {
                estoqueFabricante.Saldo -= 1; 

                await _estoqueFabricanteRepository.Update(estoqueFabricante);
                
            }
            else
            {
                throw new Exception("Estoque do fabricante insuficiente.");
            }

            var movimentacao = new MovimentacaoPvb
            {
                Codigo = saidaRoloDto.Codigo,
                Data = DateTime.Now,
                NumeroRolo = saidaRoloDto.NumeroRolo,
                Destino = saidaRoloDto.Destino,
            };

            await _movimentacaoRepository.AddAsync(movimentacao);

            await _unitOfWork.SaveAsync();

            return saidaRoloDto;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao baixar estoque", ex);
        }
    }

}

