namespace BT //within the bahaviour tree namespace
{
    public abstract class BahaviourTree //simple abstract class which runs update on nodes as well as connecting the actual tree logic to the tak class
    {
        protected Tank Owner { get; private set; } //owner class getter/ setter
        public Node Root { get; protected set; } //root / starting node

        public BahaviourTree(Tank owner) 
        {
            Owner = owner; //sets tank connection
        }
        public void Update() //updates the execute function for nodes
        {
            Root.Execute();
        }
    }

}
