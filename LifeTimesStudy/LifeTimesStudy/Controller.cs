using Microsoft.AspNetCore.Mvc;

namespace LifeTimesStudy
{
    public class ProbeController: Controller
    {
        private readonly ConsumesScoped consumesScoped;
        private readonly ScopedService scoped;

        public ProbeController(ConsumesScoped consumesScoped,ScopedService scoped)
        {
            this.consumesScoped = consumesScoped;
            this.scoped = scoped;
        }
        [HttpGet("/singleton")]
        public async Task<ActionResult> GetIdsSingleton()
        {
            return Ok($"scoped inner instance id {await consumesScoped.GetScopedsInstanceId()}, id {consumesScoped.InstanceId}");
        }
        [HttpGet("scoped")]
        public ActionResult GetIdScoped()
        {
            return Ok($"scoped id: "+scoped.InstanceId);
        }
    }
}
