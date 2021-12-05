
using System;
using System.Collections.Generic;
using TestStack.White;
using TestStack.White.WindowsAPI;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.Finders;
using System.Windows.Automation;
using TestStack.White.InputDevices;

namespace addressbook_test_white
{
    public class GroupHelper:HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public static string DELGROUPWINTITLE = "Delete group";
        public GroupHelper(ApplicationManager manager): base(manager) { }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();
            Window dialogue = OpenGroupsDialogue();
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            foreach(TreeNode item in root.Nodes) 
            {
                list.Add(new GroupData()
                {
                    Name = item.Text
                });
            }
            ClouseGroupsDialogue(dialogue);
            return list;

        }

        public void Add(GroupData newGroup)
        {
            Window dialogue = OpenGroupsDialogue();
            InitGruopCreatin(dialogue);
            TextBox textBox = (TextBox)dialogue.Get(SearchCriteria.ByControlType(ControlType.Edit));
            textBox.Enter(newGroup.Name);
            Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.RETURN);
            ClouseGroupsDialogue(dialogue);
        }
        public void Remove(int index)
        {
            Window dialogue = OpenGroupsDialogue();
            SelectGroup(index, dialogue);
            Delete(dialogue);
            ClouseGroupsDialogue(dialogue);
        }

        public void Delete(Window dialogue)
        {
            dialogue.Get<Button>("uxDeleteAddressButton").Click();
            Window dialogue2 = dialogue.ModalWindow(DELGROUPWINTITLE);
            dialogue2.Get<Button>("uxOKAddressButton").Click();
        }

        public void SelectGroup(int index, Window dialogue)
        {
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            root.Nodes[0].Select();

        }

        public static void InitGruopCreatin(Window dialogue)
        {
            dialogue.Get<Button>("uxNewAddressButton").Click();
        }

        public void ClouseGroupsDialogue(Window dialogue)
        {
            dialogue.Get<Button>("uxCloseAddressButton").Click();
        }

        public Window OpenGroupsDialogue()
        {
           manager.MainWindow.Get<Button>("groupButton").Click();
           return manager.MainWindow.ModalWindow(GROUPWINTITLE);
        }
    }
}