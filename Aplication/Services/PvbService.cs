using Application.Interfaces;
using Application.DTOs;
using Application.Interfaces.Queries;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class PvbService : IPvbService
    {
        private readonly IPvbQuery _pvbQuery;
        private readonly IPvbRepository _pvbRepository;
        public PvbService(IPvbQuery pvbQuery, IPvbRepository pvbRepository)
        {
            _pvbQuery = pvbQuery;
            _pvbRepository = pvbRepository;
        }

       
        public async Task<Pvb> Add(AddPvbDto addPvbDto)
        {
            var pvb = new Pvb(addPvbDto.CodigoPvb, addPvbDto.TipoPvb, addPvbDto.Espessura, addPvbDto.TamanhoRolo, addPvbDto.Fabricante);

            
            await _pvbRepository.AddAsync(pvb);
            await _pvbRepository.SaveChangesAsync();

            return pvb;
        }

        public async Task Update(UpdatePvbDto updatePvbDto)
        {
            var pvb = await _pvbQuery.GetById(updatePvbDto.Codigo);

            if (pvb == null)
                throw new ArgumentException("Pvb não encontrado");

            var updatePvb = new Pvb(updatePvbDto.CodigoPvb, updatePvbDto.TipoPvb, updatePvbDto.Espessura, updatePvbDto.TamanhoRolo, updatePvbDto.Fabricante);

            await _pvbRepository.Update(updatePvb);
            await _pvbRepository.SaveChangesAsync();
        }

        public async Task<bool> Delete(int codigo)
        {
            var retorno = await _pvbRepository.DeleteById(codigo);
            await _pvbRepository.SaveChangesAsync();

            return retorno;
        }

        public async Task<List<Pvb>> Get()
        {
            return await _pvbQuery.Get();
        }

        public async Task<PaginationResponse<PvbDto>> GetFilter(PaginationRequest paginationRequest)
        {
            return await _pvbQuery.GetFilter(paginationRequest);
        }

        public async Task<PvbDto> GetById(int codigo)
        {
            return await _pvbQuery.GetById(codigo);
        }

       
    }
}
