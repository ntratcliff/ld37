using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Interface for scripts that recieve callbacks for round events
/// (i.e start and finish)
/// </summary>
public interface IRoundStatusTarget : IEventSystemHandler 
{
    void RoundStart();
    void RoundEnd();
}
