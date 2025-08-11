using System.Collections.Generic;
using RuedaYPatas.Models;

namespace RuedasYPatas_II.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Hospedaje> Hospedajes { get; set; }
        public IEnumerable<Transporte> Transportes { get; set; }
    }
}