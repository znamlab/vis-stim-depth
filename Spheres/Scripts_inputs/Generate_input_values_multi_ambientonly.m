%% GENERATE INPUT FILES FOR BONSAI SPHERE STIMULATION
%  created @yiranhe
%  edited @yiranhe 20/12/29:
%   1) generate angle values --> convert to spherical XYZ for Sphere018t.bonsai

%%
root = 'E:\vis-stim\vis-stim-depth\Spheres\';
write2folder = 'Files_inputs\';
root = [root,write2folder];

%% Which one to run?
generate_translationXYZ = 0;
generate_ambient_values = 1;
ifSave = 1;

%% FOR VISUAL STIMULI TO FADE IN
% Generate a series of ambient values
if generate_ambient_values
    
    % Set values 
    FRAMERATE = 144; %Hz
    
    ambient.START = 0.5; %for background in DARKGREY
    ambient.END = 1;
    ambient.SUSTAIN = 1;

    ambient.fadein_time = 0.5; %s
    ambient.fadeout_time = 0.5; %s
    ambient.total_time = 4; %S
    ambient.sustain_time = ambient.total_time - ambient.fadein_time - ambient.fadeout_time;
    ambient.filename = 'Ambient_values_single';

    % Generate a series of ambient values
    ambient.arr1 = [linspace(ambient.START,ambient.END,(FRAMERATE * ambient.fadein_time))];
    ambient.arr2 = [ones(1,ambient.sustain_time * FRAMERATE+1) * ambient.SUSTAIN];
    ambient.arr3 = [linspace(ambient.END,ambient.START,(FRAMERATE * ambient.fadeout_time))];
    ambient.arr = [ambient.arr1,ambient.arr2,ambient.arr3]'; %needs to be in a column

    % Save to csv
    if ifSave
        ambient.T = array2table(ambient.arr);
        writetable(ambient.T,[root,ambient.filename,'.csv'],'WriteVariableNames',0);
    end

end
