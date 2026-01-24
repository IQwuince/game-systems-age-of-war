using UnityEngine;
using UnityEditor;

/// <summary>
/// Editor script to validate GameConfig values and helper methods.
/// Run via menu: Tools > Game Config > Validate Configuration
/// </summary>
public class GameConfigValidationTests : EditorWindow
{
    [MenuItem("Tools/Game Config/Validate Configuration")]
    public static void RunValidation()
    {
        Debug.Log("=== GameConfig Validation Tests ===");
        
        // Find or create a temporary GameConfig for testing
        GameConfig config = ScriptableObject.CreateInstance<GameConfig>();
        
        bool allPassed = true;
        int testsPassed = 0;
        int testsFailed = 0;
        
        // Test 1: CalculateTroopCost at level 1 returns base cost
        allPassed &= AssertEqual(
            config.CalculateTroopCost(config.soldierBaseCost, 1),
            config.soldierBaseCost,
            "CalculateTroopCost(soldierBaseCost, 1) should equal soldierBaseCost",
            ref testsPassed, ref testsFailed);
        
        // Test 2: GetMinimumCombatScoreForRound(1) returns minimumCombatScoreBase
        allPassed &= AssertEqual(
            config.GetMinimumCombatScoreForRound(1),
            config.minimumCombatScoreBase,
            "GetMinimumCombatScoreForRound(1) should equal minimumCombatScoreBase",
            ref testsPassed, ref testsFailed);
        
        // Test 3: GetMinimumCombatScoreForRound(2) returns correct scaled value
        int expectedRound2MSC = config.minimumCombatScoreBase + config.minimumCombatScorePerRound;
        allPassed &= AssertEqual(
            config.GetMinimumCombatScoreForRound(2),
            expectedRound2MSC,
            "GetMinimumCombatScoreForRound(2) should equal minimumCombatScoreBase + minimumCombatScorePerRound",
            ref testsPassed, ref testsFailed);
        
        // Test 4: CalculateUpgradeCost at level 1 returns base cost
        allPassed &= AssertEqual(
            config.CalculateUpgradeCost(config.soldierHealthUpgradeBaseCost, 1),
            config.soldierHealthUpgradeBaseCost,
            "CalculateUpgradeCost(soldierHealthUpgradeBaseCost, 1) should equal soldierHealthUpgradeBaseCost",
            ref testsPassed, ref testsFailed);
        
        // Test 5: GetSoldierStats at level 1 returns base stats
        var soldierStats = config.GetSoldierStats(1, 1);
        allPassed &= AssertEqual(
            soldierStats.health,
            config.soldierBaseHealth,
            "GetSoldierStats(1, 1).health should equal soldierBaseHealth",
            ref testsPassed, ref testsFailed);
        allPassed &= AssertEqual(
            soldierStats.damage,
            config.soldierBaseDamage,
            "GetSoldierStats(1, 1).damage should equal soldierBaseDamage",
            ref testsPassed, ref testsFailed);
        
        // Test 6: GetSoldierCombatScore returns expected value
        int expectedSoldierScore = config.soldierBaseHealth + config.soldierBaseDamage; // With default weights of 1
        allPassed &= AssertEqual(
            config.GetSoldierCombatScore(1, 1),
            expectedSoldierScore,
            "GetSoldierCombatScore(1, 1) should equal soldierBaseHealth + soldierBaseDamage (with default weights)",
            ref testsPassed, ref testsFailed);
        
        // Test 7: CalculateBonusGoldFromScore returns 0 when below minimum
        allPassed &= AssertEqual(
            config.CalculateBonusGoldFromScore(100, 50),
            0,
            "CalculateBonusGoldFromScore(100, 50) should return 0 when below minimum",
            ref testsPassed, ref testsFailed);
        
        // Test 8: CalculateDamageFromLowScore returns 0 when at or above minimum
        allPassed &= AssertEqual(
            config.CalculateDamageFromLowScore(100, 100),
            0,
            "CalculateDamageFromLowScore(100, 100) should return 0 when at minimum",
            ref testsPassed, ref testsFailed);
        
        // Test 9: GetSoldierSellPrice returns expected value
        int expectedSellPrice = Mathf.RoundToInt(config.soldierBaseCost * config.soldierSellPercent);
        allPassed &= AssertEqual(
            config.GetSoldierSellPrice(1),
            expectedSellPrice,
            "GetSoldierSellPrice(1) should equal soldierBaseCost * soldierSellPercent",
            ref testsPassed, ref testsFailed);
        
        // Test 10: CalculateDoubleTroublePercent at level 1 returns 0
        allPassed &= AssertEqual(
            (int)config.CalculateDoubleTroublePercent(1),
            0,
            "CalculateDoubleTroublePercent(1) should return 0",
            ref testsPassed, ref testsFailed);
        
        // Test 11: CalculateDoubleTroublePercent at level 2 returns doubleTroublePercentPerLevel
        allPassed &= AssertEqual(
            (int)config.CalculateDoubleTroublePercent(2),
            (int)config.doubleTroublePercentPerLevel,
            "CalculateDoubleTroublePercent(2) should return doubleTroublePercentPerLevel",
            ref testsPassed, ref testsFailed);
        
        // Test 12: Market Income Upgrade base cost default
        allPassed &= AssertEqual(
            config.marketIncomeUpgradeBaseCost,
            30,
            "marketIncomeUpgradeBaseCost default should be 30",
            ref testsPassed, ref testsFailed);
        
        // Test 13: Market Income Bonus Percent Per Level default
        allPassed &= AssertEqual(
            (int)config.marketIncomeBonusPercentPerLevel,
            10,
            "marketIncomeBonusPercentPerLevel default should be 10",
            ref testsPassed, ref testsFailed);
        
        // Clean up
        DestroyImmediate(config);
        
        // Summary
        Debug.Log($"=== Validation Complete: {testsPassed} passed, {testsFailed} failed ===");
        
        if (allPassed)
        {
            Debug.Log("<color=green>All GameConfig validation tests PASSED!</color>");
        }
        else
        {
            Debug.LogError("<color=red>Some GameConfig validation tests FAILED. Check logs above.</color>");
        }
    }
    
    private static bool AssertEqual(int actual, int expected, string message, ref int passed, ref int failed)
    {
        if (actual == expected)
        {
            Debug.Log($"<color=green>PASS:</color> {message} (value: {actual})");
            passed++;
            return true;
        }
        else
        {
            Debug.LogError($"<color=red>FAIL:</color> {message} (expected: {expected}, actual: {actual})");
            failed++;
            return false;
        }
    }
    
    [MenuItem("Tools/Game Config/Show Configuration Values")]
    public static void ShowConfigValues()
    {
        GameConfig config = ScriptableObject.CreateInstance<GameConfig>();
        
        Debug.Log("=== Default GameConfig Values ===");
        Debug.Log($"totalRounds: {config.totalRounds}");
        Debug.Log($"startingMoney: {config.startingMoney}");
        Debug.Log($"baseMarketIncome: {config.baseMarketIncome}");
        Debug.Log($"--- Scaling ---");
        Debug.Log($"upgradeScalingBase: {config.upgradeScalingBase}");
        Debug.Log($"troopScalingBase: {config.troopScalingBase}");
        Debug.Log($"--- Soldier ---");
        Debug.Log($"soldierBaseCost: {config.soldierBaseCost}");
        Debug.Log($"soldierBaseHealth: {config.soldierBaseHealth}");
        Debug.Log($"soldierBaseDamage: {config.soldierBaseDamage}");
        Debug.Log($"--- Tank ---");
        Debug.Log($"tankBaseCost: {config.tankBaseCost}");
        Debug.Log($"tankBaseHealth: {config.tankBaseHealth}");
        Debug.Log($"tankBaseDamage: {config.tankBaseDamage}");
        Debug.Log($"--- Super Troop ---");
        Debug.Log($"superTroopBaseCost: {config.superTroopBaseCost}");
        Debug.Log($"superTroopBaseHealth: {config.superTroopBaseHealth}");
        Debug.Log($"superTroopBaseDamage: {config.superTroopBaseDamage}");
        Debug.Log($"--- Double Trouble ---");
        Debug.Log($"doubleTroubleBaseCost: {config.doubleTroubleBaseCost}");
        Debug.Log($"doubleTroublePercentPerLevel: {config.doubleTroublePercentPerLevel}");
        Debug.Log($"doubleTroubleMaxPercent: {config.doubleTroubleMaxPercent}");
        Debug.Log($"--- Market Income Upgrade ---");
        Debug.Log($"marketIncomeUpgradeBaseCost: {config.marketIncomeUpgradeBaseCost}");
        Debug.Log($"marketIncomeBonusPercentPerLevel: {config.marketIncomeBonusPercentPerLevel}");
        Debug.Log($"--- MSC Settings ---");
        Debug.Log($"minimumCombatScoreBase: {config.minimumCombatScoreBase}");
        Debug.Log($"minimumCombatScorePerRound: {config.minimumCombatScorePerRound}");
        Debug.Log($"useMinimumCombatScore: {config.useMinimumCombatScore}");
        Debug.Log($"useMinimumCombatScoreAsDamage: {config.useMinimumCombatScoreAsDamage}");
        Debug.Log($"--- UI Toggles ---");
        Debug.Log($"showPredictedMSCBonusInUI: {config.showPredictedMSCBonusInUI}");
        Debug.Log($"showSellButtonsByDefault: {config.showSellButtonsByDefault}");
        
        DestroyImmediate(config);
    }
}
