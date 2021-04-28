class IngoingCountConditional : IntegerConditional
{
    public IngoingCountConditional(IReducable target) : base(target)
    {
        
    }

    public override void CalculateInternalValue()
    {
        _result = GetTarget().GetPredecessors().Count;
    }
}