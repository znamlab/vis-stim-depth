%% GENERATE INPUT FILE FOR BONSAI SPHERE STIMULATION
%  created @yiranhe
%%
root = 'E:\code\vis-stim\Yiran\';

FRAMERATE = 144; %Hz
INITIAL_DELAY = 2; %s

%% Which one to run?
generate_ambient_values = 0;
generate_translationXYZ = 1;

%% FOR VISUAL STIMULI TO FADE IN
%Generate a series of ambient values
if generate_ambient_values
    
    ambient.START = 0.664; %for background in DARKGREY
    ambient.END = 1;
    ambient.SUSTAIN = 1;
    ambient.fadein_time = 0.5; %s
    ambient.fadeout_time = 0.5; %s
    ambient.total_time = 4; %S
    ambient.sustain_time = ambient.total_time - ambient.fadein_time - ambient.fadeout_time;
    ambient.filename = 'Ambient_values';

    ambient.initialarr = [ones(1,INITIAL_DELAY*FRAMERATE)*ambient.START];
    ambient.arr1 = [linspace(ambient.START,ambient.END,(FRAMERATE*ambient.fadein_time))];
    ambient.arr2 = [ones(1,ambient.sustain_time*FRAMERATE+1)*ambient.SUSTAIN];
    ambient.arr3 = [linspace(ambient.END,ambient.START,(FRAMERATE*ambient.fadeout_time))];
    ambient.arr = [ambient.initialarr,ambient.arr1,ambient.arr2,ambient.arr3];

    %save to csv
    ambient.T = array2table(ambient.arr);
    writetable(ambient.T,[root,ambient.filename,'.csv'],'WriteVariableNames',0);

end


%% FOR VISUAL STIMULI TO FADE IN
%Generate a series of ambient values
if generate_ambient_values
