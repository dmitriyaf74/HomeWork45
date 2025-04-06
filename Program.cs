using HomeWork45.classes;
using System.Xml.Linq;
using HomeWork45.classes;

namespace HomeWork45
{
    internal class Program
    {

        static TreeNodeEmployee root = null;

        static void EnterNameSalary(ref string Name, ref int Salary) 
        {
            Console.WriteLine("Введите имя: ");
            Name = Console.ReadLine();
            if (Name == string.Empty) 
                return; 
            Console.WriteLine("Размер зп: ");
            try
            {
                Salary = int.Parse(Console.ReadLine());
            }
            catch 
            { 
                Name = string.Empty;
                return;
            }
            
        }

        static TreeNodeEmployee CreateNode(string Name, int Salary) 
        {
            /*1. принимает на вход из консоли информацию о сотрудниках: имя + зарплата 
               (имя в первой строке, зарплата в виде целого числа во второй строке; 
               и так для каждого сотрудника, пока пользователь не введет пустую строку 
               в качестве имени сотрудника)*/
            var node = new TreeNodeEmployee();
            node.Name = Name;
            node.Value = Salary;
            return node;
        }

        static void TreeAddNode(TreeNodeEmployee ParentNode, TreeNodeEmployee Node) 
        {
            /*2. попутно при получении информации о сотрудниках строится бинарное дерево с этой информацией, 
                где в каждом узле хранится имя сотрудника, а его зарплата является значением, 
                на основе которого производится бинарное разделение в дереве*/

            if (Node.Value < ParentNode.Value)
            {
                if (ParentNode.Left is null)
                    ParentNode.Left = Node;
                else
                    TreeAddNode((TreeNodeEmployee)ParentNode.Left, Node);
            }
            else 
            {
                if (ParentNode.Right is null)
                    ParentNode.Right = Node;
                else
                    TreeAddNode((TreeNodeEmployee)ParentNode.Right, Node);
            }
        }

        static void SymmetricTraversal(TreeNodeEmployee Node)
        {
            /*3. после окончания ввода пользователем программа выводит имена сотрудников и их зарплаты в порядке возрастания 
                зарплат(в каждой строчке формат вывода "Имя - зарплата").
                Использовать для этого симметричный обход дерева.*/
            if (Node.Left is not null)
                SymmetricTraversal((TreeNodeEmployee)Node.Left);
            Console.WriteLine($"{Node.Name}={Node.Value}");
            if (Node.Right is not null)
                SymmetricTraversal((TreeNodeEmployee)Node.Right);
        }


        static void AddUserNodes()
        {
            while (true)
            {
                string name = "";
                int salary = 0;
                EnterNameSalary(ref name, ref salary);
                if (name == string.Empty)
                    break;
                var node = CreateNode(name, salary);

                if (root == null)
                    root = node;
                else
                    TreeAddNode(root, node);
            }
        }

        static TreeNodeEmployee FindEmployeeBySalary(int Salary)
        {
            var node = root;
            while (node != null)
            {
                if (node.Value == Salary)
                    return node;
                if (Salary < node.Value)
                    node = (TreeNodeEmployee)node.Left;
                else
                    node = (TreeNodeEmployee)node.Right;
            }
            return null;
        }

        static void MainUserCycle()
        {
            /*после этого программа предлагает ввести цифру 0(переход к началу программы) или 1(снова поиск зарплаты).
                При вводе 0 должен произойти переход к началу работы программы, т.е.опять запрашивается список сотрудников 
                и строится новое дерево. При вводе 1 должны снова запросить зарплату, которую хочется поискать в дереве -см.предыдущий пункт.*/
            var LastUserAnswer = "1";
            while (LastUserAnswer == "1")
            {
                UserFindEmployeeBySalary();
                Console.WriteLine($"Введите 0 для завершения или 1 для повторного поиска:");
                LastUserAnswer = Console.ReadLine();
            }
        }

        static void UserFindEmployeeBySalary()
        {
            /*после этого программа запрашивает размер зарплаты, который интересует пользователя. 
                В построенном бинарном дереве программа находит сотрудника с указанной зарплатой и 
                выводит его имя. Если сотрудник не найден -выводится "такой сотрудник не найден"*/

            Console.WriteLine("Какой размер зп вас интересует?: ");
            var salary = int.Parse(Console.ReadLine());

            var node = FindEmployeeBySalary(salary);
            if (node == null)
                Console.WriteLine("такой сотрудник не найден");
            else
                Console.WriteLine(node.Name);
        }

        static void AddTestNodes()
        {
            root = CreateNode("ar", 9);
            TreeAddNode(root, CreateNode("a1", 10));
            TreeAddNode(root, CreateNode("a2", 2));
            TreeAddNode(root, CreateNode("a3", 5));
            TreeAddNode(root, CreateNode("a4", 5));
            TreeAddNode(root, CreateNode("a5", 43));
            TreeAddNode(root, CreateNode("a6", 15));
            TreeAddNode(root, CreateNode("a7", 4));

        }

        static void Main(string[] args)
        {
            AddUserNodes();
            //AddTestNodes();

            SymmetricTraversal(root);

            MainUserCycle();

        }
    }
}
