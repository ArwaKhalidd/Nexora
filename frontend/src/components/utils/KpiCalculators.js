export function computeExecutiveKPIs(students, courses) {
  const total = students.length;
  const uniqueStudents = new Set(students.map(s => s.id)).size;
  const totalCourses = courses.length;
  const pass = students.filter(s => s.res === 'Pass').length;
  const distinction = students.filter(s => s.res === 'Distinction').length;
  const fail = students.filter(s => s.res === 'Fail').length;
  const withdrawn = students.filter(s => s.res === 'Withdrawn').length;
  const passRate = total > 0 ? ((pass + distinction) / total) * 100 : 0;
  const failRate = total > 0 ? (fail / total) * 100 : 0;
  const withdrawalRate = total > 0 ? (withdrawn / total) * 100 : 0;
  const distinctionRate = total > 0 ? (distinction / total) * 100 : 0;
  const studentsWithScores = students.filter(s => s.score > 0);
  const avgScore = studentsWithScores.length > 0 ? studentsWithScores.reduce((a, s) => a + s.score, 0) / studentsWithScores.length : 0;
  return { total, uniqueStudents, totalCourses, pass, distinction, fail, withdrawn, passRate, failRate, withdrawalRate, distinctionRate, avgScore };
}

export function computeOutcomeDistribution(students) {
  return [
    { name: 'Pass', value: students.filter(s => s.res === 'Pass').length, color: '#16C47F' },
    { name: 'Fail', value: students.filter(s => s.res === 'Fail').length, color: '#F04438' },
    { name: 'Withdrawn', value: students.filter(s => s.res === 'Withdrawn').length, color: '#F59E0B' },
    { name: 'Distinction', value: students.filter(s => s.res === 'Distinction').length, color: '#7C3AED' },
  ];
}

export function computeRateByModule(students, rateType) {
  const modules = [...new Set(students.map(s => s.mod))].sort();
  return modules.map(mod => {
    const modStudents = students.filter(s => s.mod === mod);
    const total = modStudents.length;
    let count = 0;
    if (rateType === 'pass') count = modStudents.filter(s => s.res === 'Pass' || s.res === 'Distinction').length;
    else if (rateType === 'fail') count = modStudents.filter(s => s.res === 'Fail').length;
    else count = modStudents.filter(s => s.res === 'Withdrawn').length;
    return { name: mod, value: total > 0 ? parseFloat(((count / total) * 100).toFixed(1)) : 0 };
  });
}

export function computeStudentRiskKPIs(students) {
  const total = students.length;
  const highRisk = students.filter(s => s.res === 'Fail' || s.res === 'Withdrawn').length;
  const mediumRisk = students.filter(s => s.res === 'Pass').length;
  const lowRisk = students.filter(s => s.res === 'Distinction').length;
  const studentsWithClicks = students.filter(s => s.clicks > 0);
  const avgEngagement = studentsWithClicks.length > 0 ? studentsWithClicks.reduce((a, s) => a + s.clicks, 0) / studentsWithClicks.length : 0;
  return { total, highRisk, mediumRisk, lowRisk, avgEngagement };
}

export function computePrevAttemptsByResult(students) {
  const results = ['Fail', 'Withdrawn', 'Pass', 'Distinction'];
  return results.map(res => {
    const group = students.filter(s => s.res === res);
    const avg = group.length > 0 ? group.reduce((a, s) => a + s.prev, 0) / group.length : 0;
    return { name: res, value: parseFloat(avg.toFixed(3)) };
  });
}

export function computeAvgScoreByResult(students) {
  const results = ['Withdrawn', 'Fail', 'Pass', 'Distinction'];
  return results.map(res => {
    const group = students.filter(s => s.res === res && s.score > 0);
    const avg = group.length > 0 ? group.reduce((a, s) => a + s.score, 0) / group.length : 0;
    return { name: res, value: parseFloat(avg.toFixed(0)) };
  });
}

export function computeClicksByResult(students) {
  const results = ['Distinction', 'Pass', 'Withdrawn', 'Fail'];
  return results.map(res => {
    const group = students.filter(s => s.res === res && s.clicks > 0);
    const avg = group.length > 0 ? group.reduce((a, s) => a + s.clicks, 0) / group.length : 0;
    return { name: res, value: parseFloat(avg.toFixed(0)) };
  });
}

export function computeWithdrawalByCredits(students) {
  const bins = [
    { label: '30-60', min: 0, max: 60 },
    { label: '61-120', min: 61, max: 120 },
    { label: '121-180', min: 121, max: 180 },
    { label: '181-240', min: 181, max: 240 },
    { label: '240+', min: 241, max: 9999 },
  ];
  return bins.map(bin => ({
    name: bin.label,
    value: students.filter(s => s.res === 'Withdrawn' && s.cred >= bin.min && s.cred <= bin.max).length,
  }));
}

export function computePlatformKPIs(students) {
  const total = students.length;
  const engaged = students.filter(s => s.clicks > 0).length;
  const activePct = total > 0 ? (engaged / total) * 100 : 0;
  const totalClicks = students.reduce((a, s) => a + s.clicks, 0);
  const studentsWithScores = students.filter(s => s.score > 0);
  const avgScore = studentsWithScores.length > 0 ? studentsWithScores.reduce((a, s) => a + s.score, 0) / studentsWithScores.length : 0;
  const passRate = total > 0 ? (students.filter(s => s.res === 'Pass' || s.res === 'Distinction').length / total) * 100 : 0;
  return { totalEngaged: engaged, activePct, totalClicks, avgScore, passRate };
}

export function computeClicksByModule(students) {
  const modules = [...new Set(students.map(s => s.mod))].sort();
  return modules.map(mod => ({
    name: mod,
    value: students.filter(s => s.mod === mod).reduce((a, s) => a + s.clicks, 0),
  })).sort((a, b) => b.value - a.value);
}

export function computeAvgClicksByModule(students) {
  const modules = [...new Set(students.map(s => s.mod))].sort();
  return modules.map(mod => {
    const modStudents = students.filter(s => s.mod === mod && s.clicks > 0);
    const avg = modStudents.length > 0 ? modStudents.reduce((a, s) => a + s.clicks, 0) / modStudents.length : 0;
    return { name: mod, value: parseFloat(avg.toFixed(0)) };
  }).sort((a, b) => b.value - a.value);
}

export function computeAvgScoreByModule(students) {
  const modules = [...new Set(students.map(s => s.mod))].sort();
  return modules.map(mod => {
    const group = students.filter(s => s.mod === mod && s.score > 0);
    const avg = group.length > 0 ? group.reduce((a, s) => a + s.score, 0) / group.length : 0;
    return { name: mod, value: parseFloat(avg.toFixed(0)) };
  }).sort((a, b) => b.value - a.value);
}

export function computeModuleRanking(students) {
  const modules = [...new Set(students.map(s => s.mod))];
  return modules.map(mod => ({
    name: mod,
    value: students.filter(s => s.mod === mod).length,
  })).sort((a, b) => b.value - a.value);
}

export function computeActivityTypeUsage(assessmentTypes) {
  const total = assessmentTypes.reduce((a, t) => a + t.count, 0);
  const colors = { TMA: '#009EF7', CMA: '#17C9D3', Exam: '#7C3AED' };
  return assessmentTypes.map(t => ({
    name: t.type,
    value: t.count,
    pct: total > 0 ? parseFloat(((t.count / total) * 100).toFixed(2)) : 0,
    color: colors[t.type] || '#94A3B8',
  }));
}

export function generateInsights(students) {
  const insights = [];
  const total = students.length;
  if (total === 0) return insights;
  const passRate = (students.filter(s => s.res === 'Pass' || s.res === 'Distinction').length / total * 100);
  const wdRate = (students.filter(s => s.res === 'Withdrawn').length / total * 100);
  insights.push(`Overall pass rate is ${passRate.toFixed(1)}% across ${total.toLocaleString()} student registrations.`);
  insights.push(`Withdrawal rate stands at ${wdRate.toFixed(1)}%, affecting ${students.filter(s => s.res === 'Withdrawn').length.toLocaleString()} students.`);

  const modules = [...new Set(students.map(s => s.mod))];
  const modRates = modules.map(mod => {
    const ms = students.filter(s => s.mod === mod);
    return { mod, passRate: ms.length > 0 ? (ms.filter(s => s.res === 'Pass' || s.res === 'Distinction').length / ms.length * 100) : 0, wdRate: ms.length > 0 ? (ms.filter(s => s.res === 'Withdrawn').length / ms.length * 100) : 0 };
  });
  const best = modRates.sort((a, b) => b.passRate - a.passRate)[0];
  const worst = modRates.sort((a, b) => a.passRate - b.passRate)[0];
  const highestWD = modRates.sort((a, b) => b.wdRate - a.wdRate)[0];
  insights.push(`Module ${best.mod} has the highest pass rate at ${best.passRate.toFixed(1)}%.`);
  insights.push(`Module ${worst.mod} has the lowest pass rate at ${worst.passRate.toFixed(1)}%.`);
  insights.push(`Module ${highestWD.mod} has the highest withdrawal rate at ${highestWD.wdRate.toFixed(1)}%.`);

  const avgS = students.filter(s => s.score > 0);
  if (avgS.length > 0) {
    const avg = avgS.reduce((a, s) => a + s.score, 0) / avgS.length;
    insights.push(`Average academic score is ${avg.toFixed(1)} out of 100.`);
  }
  return insights;
}

export function computeAcademicKPIs(students) {
  const total = students.length;
  const withScores = students.filter(s => s.score > 0);
  const avgScore = withScores.length > 0 ? withScores.reduce((a, s) => a + s.score, 0) / withScores.length : 0;
  const withAssessments = students.filter(s => s.nass > 0);
  const avgAssessments = withAssessments.length > 0 ? withAssessments.reduce((a, s) => a + s.nass, 0) / withAssessments.length : 0;
  const distinctionRate = total > 0 ? (students.filter(s => s.res === 'Distinction').length / total) * 100 : 0;
  const scoreBins = [0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100];
  const scoreDistribution = [];
  for (let i = 0; i < scoreBins.length - 1; i++) {
    const lo = scoreBins[i];
    const hi = scoreBins[i + 1];
    scoreDistribution.push({
      name: `${lo}-${hi}`,
      value: withScores.filter(s => s.score >= lo && s.score < (hi === 100 ? 101 : hi)).length,
    });
  }
  const eduLevels = [...new Set(students.map(s => s.edu))];
  const scoreByEdu = eduLevels.map(edu => {
    const group = students.filter(s => s.edu === edu && s.score > 0);
    return {
      name: edu.replace(' or Equivalent', '').replace(' Qualification', ''),
      value: group.length > 0 ? parseFloat((group.reduce((a, s) => a + s.score, 0) / group.length).toFixed(1)) : 0,
    };
  }).sort((a, b) => b.value - a.value);
  return { avgScore, avgAssessments, distinctionRate, scoreDistribution, scoreByEdu };
}