class OutgoingCountConditional : IntegerConditional
{
    public OutgoingCountConditional(IReducable target) : base(target)
    {
        
    }

    public override void CalculateInternalValue()
    {
        _result = GetTarget().GetSuccessors().Count;
    }
}