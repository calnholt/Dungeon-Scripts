using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour 
{
	[SerializeField]
	private GameObject managers;
	private LevelManager lm;
	private CheckpointManager cpm;
	private SectionManager[] sm;
    private Character character;

	void Start()
	{
		cpm = managers.GetComponent<CheckpointManager>();
		lm = managers.GetComponent<LevelManager>();
		sm = lm.GetSectionManagers();
        character = GetComponent<Character>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Death"))
		{
            character.DestroyEquipmentItems();
            character.Interaction.PutDownCarryingObject();
            if (sm.Length > 0)
                sm[cpm.GetCurrentCheckpoint()].ResetSection();
			character.CenterCamera(character, cpm.GetCurrentCheckpointPosition(), true);
                //ResetAllSections();
		}
	}

    private void ResetAllSections()
    {
        for (int i = 0; i < lm.sectionManagers.Length; i++)
        {
            sm[i].ResetSection();
        }
    }

}
